using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedometerAngle : MonoBehaviour {
	private float degFarLeft = 30.0f;
	private float degFarRight = -210.0f;
	private float normalized = 0.0f;

	public float maxSpeed = 100.0f;
	public float speedScaleTune = 7.0f;

	Transform playerObj;
	private Vector3 prevPosition;

	void Start() {
		playerObj = GameObject.FindGameObjectWithTag("Player").transform;
		prevPosition = playerObj.transform.position;
	}

	public void SetNeedle(float currentSpeed) {
		float normalBefore = normalized;
		if(currentSpeed < 0.0f) {
			normalized = 0.0f; // note: does not have a way to represent negatives/reverse
		} else {
			normalized = currentSpeed / maxSpeed;
			if(normalized > 1.0f) { // cap out
				normalized = 1.0f;
			}
		}
		normalized = Mathf.Lerp(normalBefore, normalized, 0.1f);
	}

	void Update () {
		if(normalized > 1.0f) {
			normalized = 0.0f;
		}
		float degRot = normalized * degFarRight + (1.0f - normalized) * degFarLeft;
		transform.rotation = Quaternion.AngleAxis(degRot, Vector3.forward);
	}

	void FixedUpdate() { // calling from here for consistency, since Lerp smoothing in SetNeedle
		SetNeedle( (prevPosition - playerObj.transform.position).magnitude * speedScaleTune / Time.deltaTime );
		prevPosition = playerObj.transform.position;
	}
}
