using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] private float smoothSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.transform.position, smoothSpeed * Time.deltaTime);
        newPosition.z = -10f;
        transform.position = newPosition;
    }
}
