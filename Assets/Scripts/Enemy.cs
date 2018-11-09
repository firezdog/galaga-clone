using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] List<Transform> waypoints;

	// Use this for initialization
	void Start () {
		transform.position = waypoints[0].position;
	}
	
	// Update is called once per frame
	void Update () {
	}

}
