using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	
	[SerializeField] private float speed = 10;

	void Awake() {
	}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		move();
	}

    private void move()
    {
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

}
