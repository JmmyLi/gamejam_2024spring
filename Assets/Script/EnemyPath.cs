using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public enum Behavior
    {
        waitTurn,
        instantTurn,
        notTurn, 
        constantTurnClockwise,
        constantTurnAnticlockwise
    }
    [SerializeField] public GameObject path;
    
    private Transform[] waypoints;
    public int _currentWaypointIndex = 0;
    [SerializeField] public float _speed = 2f;
    public Vector3 respawnLocation;
    public float _rotateSpeed;
    public Behavior behavior;
    public bool isStart = false;
    public float stopTime;
    public float stopCountDown;

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
        switch (behavior)
        {
            case Behavior.constantTurnClockwise:
                transform.Rotate(new Vector3(0, 0, -_rotateSpeed * Time.deltaTime)); break;
            case Behavior.constantTurnAnticlockwise:
                transform.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime)); break;
        }
        if (path != null)
        {
            if (stopCountDown > 0)
            {
                stopCountDown -= Time.deltaTime;
            }
            Transform wp = waypoints[_currentWaypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
                stopCountDown = stopTime;
            }
            else if (stopCountDown <= 0)
            {
                Vector3 dir = wp.position - transform.position;
                float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);
                switch (behavior)
                {
                    case Behavior.waitTurn:
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
                        break;
                    case Behavior.instantTurn:
                        transform.rotation = targetRotation;
                        transform.position = Vector3.MoveTowards(
                        transform.position,
                        wp.position,
                        _speed * Time.deltaTime);
                        break;
                    case Behavior.constantTurnAnticlockwise:
                    case Behavior.constantTurnClockwise:
                    case Behavior.notTurn:
                        transform.position = Vector3.MoveTowards(
                        transform.position,
                        wp.position,
                        _speed * Time.deltaTime);
                        break;
                }
            }
        }
    }
}
