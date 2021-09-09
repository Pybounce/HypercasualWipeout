using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNodeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private OrientedPoint[] waypoints;
    private int currentWaypoint = 0;
    private bool completedWaypoints = false;
    private Vector3 direction = new Vector3();

    private void Start()
    {
        waypoints = FindObjectOfType<GameManager>().GetSpline().GetEvenlySpacedPoints();
        transform.position = waypoints[0].point;
        direction = waypoints[currentWaypoint + 1].point - waypoints[currentWaypoint].point;
        direction.Normalize();


    }

    private void Update()
    {
        if (!completedWaypoints)
        {
            MoveWaypointNode();
        }
    }
    private void MoveWaypointNode()
    {
        Vector3 nextWaypointVector = waypoints[currentWaypoint + 1].point - transform.position;
        Vector3 nextMoveVector = speed * Time.deltaTime * direction;
        float nextWaypointSqrMag = nextWaypointVector.sqrMagnitude;
        float nextMoveSqrMag = nextMoveVector.sqrMagnitude;
        CalculateWaypointNodeRotation(Mathf.Sqrt(nextWaypointSqrMag));
        if (nextMoveSqrMag > nextWaypointSqrMag)
        {
            transform.position += nextWaypointVector;
            IncrementWaypoint();
            return;
        }
        transform.position += speed * Time.deltaTime * direction;
    }
    private void CalculateWaypointNodeRotation(float _playerMag)
    {
        float t = 1 - (_playerMag / 1f);
        transform.rotation = Quaternion.Lerp(waypoints[currentWaypoint].rotation, waypoints[currentWaypoint + 1].rotation, t);
    }
    private void IncrementWaypoint()
    {
        currentWaypoint += 1;
        if (currentWaypoint >= waypoints.Length - 2)
        {
            completedWaypoints = true;
        }
        else
        {
            direction = waypoints[currentWaypoint + 1].point - waypoints[currentWaypoint].point;
            direction.Normalize();
        }

    }

}
