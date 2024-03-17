using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public static Vector3[] interactpos;
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
        interactpos = new Vector3[] { sprite.transform.position, worldPos};
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        interactpos[0] = sprite.transform.position;
        interactpos[1] = worldPos;
        lr.SetPositions(interactpos);
    }
}
