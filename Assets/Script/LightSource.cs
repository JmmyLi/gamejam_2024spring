using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSource : MonoBehaviour
{
    [SerializeField] private Light2D light2d;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
    }

    public bool lighten(GameObject target)
    {
        Vector3 direction = transform.up;
        Vector3 dir = (target.transform.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, target.transform.position) < light2d.pointLightOuterRadius)
        {
            if (Vector3.Angle(direction, dir) < light2d.pointLightOuterAngle / 2)
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, light2d.pointLightOuterRadius, LayerMask.GetMask("Physical"));
                if (ray.collider.gameObject == target)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
