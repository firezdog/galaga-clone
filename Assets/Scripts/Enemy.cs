using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float speed = 5f;
    public void setMoveSpeed (float newSpeed) { speed = newSpeed; }
    List<Transform> waypoints;
    public void setWaypoints (List<Transform> newWaypoints) { waypoints = newWaypoints; }
    int waypointIndex = 0;

    // Use this for initialization
    void Start () {
        speed *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
        if (waypoints != null && waypoints.Count > 0) { Move (); }
    }

    private void Move () {
        Vector3 currPos = transform.position;
        Vector3 waypoint = waypoints[waypointIndex].position;
        transform.position = Vector3.MoveTowards (currPos, waypoint, speed);
        if (currPos == waypoint) {
            waypointIndex = (waypointIndex + 1); // % waypoints.Count;
        }
        if (waypointIndex == waypoints.Count) {
            Destroy (gameObject);
        }
    }

}