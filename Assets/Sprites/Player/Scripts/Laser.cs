using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	
	[SerializeField] private float speed = 10;
	Camera gameCamera;

	// Use this for initialization
	void Start () 
	{
		gameCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		move();
		eliminate();
	}

    private void move()
    {
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

	private void eliminate()
	{
		var currentY = transform.position.y;
		var maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, gameCamera.nearClipPlane)).y;
		if (currentY > maxY) {
			Destroy(gameObject);
		}
	}

}
