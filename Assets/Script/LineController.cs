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
        Debug.Log(interactpos.Length);
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
        lr.SetPositions(interactpos);
    }
}
