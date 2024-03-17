using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] public GameObject path;
    
    private Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    [SerializeField] public float _speed = 2f;
    public float _rotateSpeed;
    public bool isStart = false;

    void Start()
    {
        if (path != null)
        {
            Transform[] transforms = path.GetComponentsInChildren<Transform>();
            waypoints = transforms.Skip(1).ToArray();
        }
    }

    void FixedUpdate()
    {
        if (path != null)
        {
            Transform wp = waypoints[_currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                Vector3 dir = wp.position - transform.position;
                float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);
                if ((transform.up.normalized - dir.normalized).magnitude < 0.1f)
                {
                    transform.rotation = targetRotation;
                    transform.position = Vector3.MoveTowards(
                    transform.position,
                    wp.position,
                    _speed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
                }
                //if (isStart)
                //{
                //}
                //else isStart = true;
            }
        }
    }
}
