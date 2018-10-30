using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] public float speed = 10.0f;
	[SerializeField] public float bottomLimit = -8.8f;
	[SerializeField] public float topLimit = -3f;
	[SerializeField] public float leftLimit = -4.5f;
	[SerializeField] public float rightLimit = 4.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

    private void Move()
    {
        float speedDeltaTime = speed * Time.deltaTime;
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		float xDelta = Input.GetAxis("Horizontal") * speedDeltaTime;
		float yDelta = Input.GetAxis("Vertical") * speedDeltaTime;
		float newX = Mathf.Clamp(x + xDelta, leftLimit, rightLimit);
		float newY = Mathf.Clamp(y + yDelta, bottomLimit, topLimit);
		transform.position = new Vector3(newX, newY, z);
    }
}
