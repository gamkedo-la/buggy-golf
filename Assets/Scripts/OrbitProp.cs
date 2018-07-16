using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitProp : MonoBehaviour {

	public Vector3 center;
	public Vector3 axis = Vector3.up;
	public Vector3 desiredPosition;
	public float radius = 2.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 80.0f;

	void Start () {
		transform.position = (transform.position - center).normalized * radius + center;
	}

	void Update () {
		transform.RotateAround (center, axis, rotationSpeed * Time.deltaTime);
		desiredPosition = (transform.position - center).normalized * radius + center;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
	}
}
