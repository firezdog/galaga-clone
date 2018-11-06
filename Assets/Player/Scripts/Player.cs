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
	[NonSerialized] float bottomLimit;
	[NonSerialized] float topLimit;
	[NonSerialized] float leftLimit;
	[NonSerialized] float rightLimit;
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
		ManageFire();
		Move();
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
			Instantiate(
				laser, 
				gameObject.transform.position, 
				Quaternion.identity);
			yield return new WaitForSeconds(fireRate);
		}
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
		// this could be cleaned up a bit?
        float speedDeltaTime = speed * Time.deltaTime;
		float xDelta = Input.GetAxis("Horizontal") * speedDeltaTime;
		float yDelta = Input.GetAxis("Vertical") * speedDeltaTime;
		float newX = Mathf.Clamp(transform.position.x + xDelta, leftLimit, rightLimit);
		float newY = Mathf.Clamp(transform.position.y + yDelta, bottomLimit, topLimit);
		transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
