using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Vector3[] interactpos = new Vector3[] {Vector3.zero, Vector3.zero};
    public Vector3 worldPos;
    public GameObject sprite;
    LineRenderer lr;
    public GameObject dart;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        interactpos = new Vector3[] { Vector3.zero, Vector3.zero };
        interactpos[0] = sprite.transform.position;
        Vector3 dir = (worldPos - sprite.transform.position).normalized;
        RaycastHit2D ray = Physics2D.Raycast(sprite.transform.position, dir, 100, LayerMask.GetMask("Physical"));
        if (ray.collider != null)
        {
            interactpos[1] = ray.point;
        }
        else
        {
            interactpos[1] = worldPos;
        }
        if (Input.GetMouseButtonDown(0))
        {
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);
            if (ray.collider != null)
            {
                GameObject newDart = Instantiate(dart, ray.point, targetRotation, ray.collider.transform);
                newDart.transform.localScale = new Vector3(0.2f / newDart.transform.parent.localScale.x, 0.5f / newDart.transform.parent.localScale.y, 1 / newDart.transform.parent.localScale.z);
                sprite.GetComponent<PlayerController>().spawns.Add(newDart);
            }
            else
            {
                GameObject newDart = Instantiate(dart, worldPos, targetRotation);
                sprite.GetComponent<PlayerController>().spawns.Add(newDart);
            }
        }
        lr.SetPositions(interactpos);
    }
}
