using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] bool testing = false;
	[SerializeField] public float speed = 10.0f;
	[SerializeField, Range(0.4f,1)] public float relTopLimit = 0.4f;
	[SerializeField, Range(0,0.1f)] public float bottomMargin = 0.05f;
	[SerializeField, Range(0,0.1f)] public float leftRightMargin = 0.1f;
	// the relative top limit in terms of game screen
	// absolute limits in terms of game units
	[NonSerialized] public float bottomLimit;
	[NonSerialized] public float topLimit;
	[NonSerialized] public float leftLimit;
	[NonSerialized] public float rightLimit;

	// Use this for initialization
	void Start () {
		SetUpMoveBoundaries();
	}
	
	// Update is called once per frame
	void Update () {
		if(testing) { SetUpMoveBoundaries(); }
		Move();
	}

	private void SetUpMoveBoundaries() {
		Camera camera = FindObjectOfType<Camera>();
		Vector3 lowerLeft = camera.ViewportToWorldPoint(new Vector3(0 + leftRightMargin, 0 + bottomMargin, camera.nearClipPlane));
		Vector3 upperRight = camera.ViewportToWorldPoint(new Vector3(1 - leftRightMargin, relTopLimit, camera.nearClipPlane));
		bottomLimit = lowerLeft.y;
		topLimit = upperRight.y;
		leftLimit = lowerLeft.x;
		rightLimit = upperRight.x;
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
