using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float distance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float edgeDelta;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(target.transform.position, transform.position);

        if (Input.mousePosition.x >= Screen.width - edgeDelta)      // Check if on the right edge
        { 
            transform.position += Vector3.right * Time.deltaTime * speed;
        }

        else if (Input.mousePosition.x <= edgeDelta)      // Check if on the left edge

        { transform.position += Vector3.left * Time.deltaTime * speed; }

        else if (Input.mousePosition.y >= Screen.height - edgeDelta)      // Check if on the top edge

        { transform.position += Vector3.up * Time.deltaTime * speed; }

        else if (Input.mousePosition.y <= edgeDelta)      // Check if on the bottom edge

        { transform.position += Vector3.down * Time.deltaTime * speed; }

        else
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, target.transform.position, smoothSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
        transform.position = new Vector3 (Mathf.Clamp(transform.position.x, target.transform.position.x - maxDistance, target.transform.position.x + maxDistance), Mathf.Clamp(transform.position.y, target.transform.position.y - maxDistance, target.transform.position.y + maxDistance), -10);
    }
}
