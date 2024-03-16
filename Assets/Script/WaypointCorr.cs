using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCorr : MonoBehaviour
{   
    public Vector3 mov;
    public bool isStart;

    void startSleep() { }
    
    // Start is called before the first frame update
    void Start()
    {
        mov = GameObject.Find("enemy").transform.position - GameObject.Find("point_1").transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        
        transform.position = mov;
    }
}
