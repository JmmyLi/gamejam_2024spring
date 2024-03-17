using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] public float brightViewDistance;
    [SerializeField] public float brightViewAngle;
    [SerializeField] public float darkViewDistance;
    [SerializeField] public float darkViewAngle;
    private Vector3 direction;
    [SerializeField] public Light2D brightViewCone;
    [SerializeField] public Light2D darkViewCone;
    [SerializeField] private GameObject player;
    public bool warn;

    private void Start()
    {
        

        warn = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        brightViewCone.pointLightInnerRadius = brightViewDistance;
        brightViewCone.pointLightOuterRadius = brightViewDistance + 0.5f;
        brightViewCone.pointLightInnerAngle = brightViewAngle;
        brightViewCone.pointLightOuterAngle = brightViewAngle + 10f;

        darkViewCone.pointLightInnerRadius = darkViewDistance;
        darkViewCone.pointLightOuterRadius = darkViewDistance;
        darkViewCone.pointLightInnerAngle = darkViewAngle;
        darkViewCone.pointLightOuterAngle = darkViewAngle;
        if (!warn)
        {
            brightViewCone.intensity = Mathf.Clamp(-Mathf.Cos(player.GetComponent<PlayerController>().timer.time / (24 * 60) * 2 * Mathf.PI) - 0.5f, 0, 1) * 1;
            darkViewCone.intensity = Mathf.Clamp(-Mathf.Cos(player.GetComponent<PlayerController>().timer.time / (24 * 60) * 2 * Mathf.PI) - 0.5f, 0, 1) * 1;

            direction = transform.up;
            Vector3 dir = (player.transform.position - transform.position).normalized;
            if (player.GetComponent<PlayerController>().bright && Vector3.Distance(transform.position, player.transform.position) < brightViewDistance)
            {
                if (Vector3.Angle(direction, dir) < brightViewAngle / 2)
                {
                    RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, brightViewDistance, LayerMask.GetMask("Physical"));
                    if (ray.collider != null && ray.collider.gameObject == player)
                    {
                        player.GetComponent<PlayerController>().Die();
                        brightViewCone.intensity = 1;
                        warn = true;
                    }
                }
            }
            if (Vector3.Distance(transform.position, player.transform.position) < darkViewDistance)
            {
                if (Vector3.Angle(direction, dir) < darkViewAngle / 2)
                {
                    RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, darkViewDistance, LayerMask.GetMask("Physical"));
                    if (ray.collider != null && ray.collider.gameObject == player)
                    {
                        player.GetComponent<PlayerController>().Die();
                        darkViewCone.intensity = 1;
                        warn = true;
                    }
                }
            }
        }
    }
}
