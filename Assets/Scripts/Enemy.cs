using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] float speed = 5;
	[SerializeField] List<Transform> waypoints;
	int waypointIndex = 0;

	// Use this for initialization
	void Start () {
		speed *= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		
	}

    private void Move()
    {
		Vector2 currPos = transform.position;
		Vector2 waypoint = waypoints[waypointIndex].position;
        transform.position = Vector2.MoveTowards(currPos, waypoint, speed);
		if (currPos == waypoint) {
			waypointIndex = (waypointIndex + 1) % waypoints.Count;
		}
    }

}
