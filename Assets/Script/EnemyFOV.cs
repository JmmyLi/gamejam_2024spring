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
    [SerializeField] private Light2D brightViewCone;
    [SerializeField] private Light2D darkViewCone;
    [SerializeField] private GameObject player;

    private void Start()
    {
        brightViewCone.pointLightInnerRadius = brightViewDistance;
        brightViewCone.pointLightOuterRadius = brightViewDistance + 0.5f;
        brightViewCone.pointLightInnerAngle = brightViewAngle;
        brightViewCone.pointLightOuterAngle = brightViewAngle + 10f;

        darkViewCone.pointLightInnerRadius = darkViewDistance;
        darkViewCone.pointLightOuterRadius = darkViewDistance;
        darkViewCone.pointLightInnerAngle = darkViewAngle;
        darkViewCone.pointLightOuterAngle = darkViewAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.up;
        Vector3 dir = (player.transform.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.transform.position) < brightViewDistance)
        {
            if (Vector3.Angle(direction, dir) < brightViewAngle / 2)
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, brightViewDistance, LayerMask.GetMask("Physical"));
                if (ray.collider.gameObject == player)
                {
                    player.GetComponent<PlayerController>().Die();
                }
            }
        }
        if (Vector3.Distance(transform.position, player.transform.position) < darkViewDistance)
        {
            if (Vector3.Angle(direction, dir) < darkViewAngle / 2)
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, darkViewDistance, LayerMask.GetMask("Physical"));
                if (ray.collider.gameObject == player)
                {
                    player.GetComponent<PlayerController>().Die();
                }
            }
        }
    }
}
