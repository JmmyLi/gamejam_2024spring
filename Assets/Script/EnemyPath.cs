using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] public GameObject path;
    
    public Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;
    public float _rotateSpeed;
    public bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        //path.transform.position = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            Vector3 dir = wp.position - transform.position;
            if ((transform.up.normalized - dir.normalized).magnitude < 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                transform.position,
                wp.position,
                _speed * Time.deltaTime);
            }
            if (isStart) transform.up = Vector3.RotateTowards(transform.up, dir.normalized, _rotateSpeed * Time.deltaTime, 0f);
            else isStart = true;
            //Quaternion toRotation = Quaternion.FromToRotation(transform.position,dir);

            //transform.rotation = Quaternion.LookRotation(dir, Vector3.forward);
        }
    }
}
