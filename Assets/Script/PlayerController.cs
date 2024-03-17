using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    [Header("Respawn")]
    public Timer timer;
    public GameObject checkPoint;
    public float respawnDelay;
    public float respawnCountDown = 0;
    public SpriteRenderer spriteRenderer;

    [Header("Light")]
    public Light2D playerLight;
    public ShadowCaster2D shadowCaster;
    public Color color;
    public Color deadColor;
    public float intensity;
    public float deadIntensity;
    public float radius;
    public float deadRadius;

    public GameObject sun;
    private Light2D sunLight;

    public GameObject[] enemies;
    public GameObject[] lightSources;
    public GameObject[] bushes;

    public bool bright;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer.Restart(6);
        timer = GetComponent<Timer>();
        playerLight = GetComponentInChildren<Light2D>();
        shadowCaster = GetComponent<ShadowCaster2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sunLight = sun.GetComponent<Light2D>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        lightSources = GameObject.FindGameObjectsWithTag("Light");
        bushes = GameObject.FindGameObjectsWithTag("Bush");
    }

    void Update()
    {
        if (respawnCountDown > 0)
        {
            respawnCountDown -= Time.deltaTime;
            if (respawnCountDown <= 0)
            {
                Respawn();
            }
        }
        else
        {
            DetectLight();
            ProcessInputs();
            Move();
            if (Input.GetKeyDown("t"))
            {
                Respawn();
                timer.Restart(8);
            }
            if (Input.GetKeyDown("r"))
            {
                Respawn();
            }
            if (Input.GetKeyDown("y"))
            {
                Respawn();
                timer.Restart(16);
            }
            LightControl();
        }
        SunControl();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void Respawn()
    {
        rb.drag = 0;
        respawnCountDown = 0;
        if (checkPoint == null)
        {
            transform.position = Vector3.zero;
            timer.Restart(8);
        }
        else
        {
            transform.position = checkPoint.GetComponent<CheckPoint>().respawnPosition;
            timer.Restart(checkPoint.GetComponent<CheckPoint>().respawnTime);
        }
        spriteRenderer.enabled = true;
        shadowCaster.enabled = true;
        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position = enemy.GetComponent<EnemyPath>().respawnLocation;
            enemy.transform.rotation = Quaternion.identity;
            enemy.GetComponentInChildren<EnemyPath>()._currentWaypointIndex = 0;
            enemy.GetComponentInChildren<EnemyFOV>().warn = false;
        }
    }

    public void LightControl()
    {
        playerLight.color = color;
        playerLight.intensity = Mathf.Clamp(Mathf.Cos(timer.time / (24 * 60) * 2 * Mathf.PI) + 0.5f, 0, 1) * intensity;
        playerLight.pointLightOuterRadius = radius;
    }

    public void SunControl()
    {
        sun.transform.position = new Vector3((((timer.time % (24 * 60)) / (24 * 60)) - 0.5f) * 1500, 250, 0);
        sunLight.intensity = Mathf.Clamp(-Mathf.Cos(timer.time / (24 * 60) * 2 * Mathf.PI) - 0.5f, 0, 1) * 2;
        sunLight.color = new Color(1, Mathf.Clamp(-Mathf.Cos(timer.time / (24 * 60) * 2 * Mathf.PI) - 0.5f, 0.1f, 1) * 0.5f, 0, 1);
    }

    public void Die()
    {
        if (respawnCountDown <= 0)
        {
            spriteRenderer.enabled = false;
            shadowCaster.enabled = false;
            rb.drag = 1000;
            respawnCountDown = respawnDelay;
            playerLight.color = deadColor;
            playerLight.intensity = deadIntensity;
            playerLight.pointLightOuterRadius = deadRadius;
        }
    }

    public void DetectLight()
    {
        bright = false;
        foreach (GameObject light in lightSources)
        {
            if (light.GetComponent<LightSource>().lighten(gameObject))
            {
                bright = true; break;
            }
        }
        if (Mathf.Clamp(-Mathf.Cos(timer.time / (24 * 60) * 2 * Mathf.PI) - 0.5f, 0, 1) > 0)
        {
            bright = true;
        }
        foreach (GameObject bush in bushes)
        {
            if (bush.GetComponent<Bush>().collided)
            {
                bright = false; break;
            }
        }
    }
}
