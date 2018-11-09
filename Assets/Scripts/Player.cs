using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] bool testing = false;
	[SerializeField] float speed = 10.0f;
	// the relative top limit in terms of game screen
	[SerializeField, Range(0.4f,1)] float relTopLimit = 0.4f;
	[SerializeField, Range(0,0.1f)] float bottomMargin = 0.05f;
	[SerializeField, Range(0,0.1f)] float leftRightMargin = 0.1f;
	// absolute limits in terms of game units
	[NonSerialized] Vector2 bottomLeftLimit;
	[NonSerialized] Vector2 topRightLimit;
	// other objects
	[SerializeField] GameObject laser;
	[SerializeField, Range(0,1)] float fireRate = 0.5f;

	// Use this for initialization
	void Start () {
		SetUpMoveBoundaries();
	}
	
	// Update is called once per frame
	void Update () {
		if (testing) { SetUpMoveBoundaries(); }
		Move();
		ManageFire();
	}

	private void ManageFire() {
		if (Input.GetButtonDown("Fire1")) {
			StartCoroutine("Fire");
		}
		if (Input.GetButtonUp("Fire1")) {
			StopCoroutine("Fire");
		}
	}

	private IEnumerator Fire () {
		while (true) {
			var newLaser = Instantiate(
				laser, 
				gameObject.transform.position, 
				Quaternion.identity);
			yield return new WaitForSeconds(fireRate);
		}
	}

	private void SetUpMoveBoundaries() {
		Camera camera = FindObjectOfType<Camera>();
		var clipPoint = camera.nearClipPlane;
		bottomLeftLimit = camera.ViewportToWorldPoint(
			new Vector3(
				leftRightMargin, 
				bottomMargin, 
				clipPoint));
		topRightLimit = camera.ViewportToWorldPoint(
			new Vector3(
				1 - leftRightMargin, 
				relTopLimit, 
				clipPoint));
	}

    private void Move()
    {
		Vector2 motionVector = getMotionInput();
		float newX = Mathf.Clamp(
			transform.position.x + motionVector.x, 
			bottomLeftLimit.x, 
			topRightLimit.x);
		float newY = Mathf.Clamp(
			transform.position.y + motionVector.y, 
			bottomLeftLimit.y, 
			topRightLimit.y);
		transform.position = new Vector3(newX, newY, transform.position.z);
    }

	private Vector2 getMotionInput() {
		float speedDeltaTime = speed * Time.deltaTime;
		float xDelta = Input.GetAxis("Horizontal") * speedDeltaTime;
		float yDelta = Input.GetAxis("Vertical") * speedDeltaTime;
		return new Vector2(xDelta, yDelta);
	}

}
