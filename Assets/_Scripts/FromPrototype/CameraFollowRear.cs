using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRear : MonoBehaviour {

	public Transform followTarget;
	public Transform lookAtTarget;
	Vector3 offset;
	//Vector3 posReset;
	public float followSmoothTime = 0.3f;
	public float stickInputFactor = 1;
	private Vector3 velocity = Vector3.zero;
	public bool lookAt = true;
	//public float offsetXRotation;

	void Start(){
		offset = transform.position - followTarget.transform.position;
	}

	void FixedUpdate() {
		//Transform offsetTarget = new Vect
		if (lookAt == true){
			transform.LookAt(lookAtTarget, Vector3.up);
		}

		//transform.rotation = target.rotation;
		transform.position = Vector3.SmoothDamp (transform.position, (followTarget.transform.position + offset), ref velocity, followSmoothTime);
		//posReset = transform.position;
		if (Input.GetAxis ("CameraX") != 0) {
			transform.Translate (Vector3.right * Input.GetAxis ("CameraX") * stickInputFactor);
		} else {
			if (transform.rotation != followTarget.rotation) {
				//transform.position = Vector3.SmoothDamp (transform.position, (transform.transform.forward + offset), ref velocity, followSmoothTime);
				//transform.RotateAround (target.position, Vector3.up, (target.transform.rotation.y - transform.rotation.y));	
				Debug.Log ("Auto-Turning!");
			}
		}
			
		// IS IT POSSIBLE TO STORE A RELATIVE POSITION TO MAINTAIN THE OFFSET?

			





		//transform.Translate (posReset, target);
		// This is how we'll rotate with the controller
		//	transform.Translate(Vector3.right * Axis);
	
	}

	void LateUpdate(){
		// Set the camera position to the players + an offset
		//transform.position = target.transform.position + offset;
	}
}