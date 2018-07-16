using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	Vector3 offset;
	//Vector3 posReset;
	public float followSmoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	//public float offsetXRotation;

	void Start(){
		offset = transform.position - target.transform.position;
	}

	void Update() {
		
		transform.LookAt(target, Vector3.up);
		
		transform.position = Vector3.SmoothDamp (transform.position, (target.transform.position + offset), ref velocity, followSmoothTime);
		
		if (Input.GetAxis ("Mouse X") != 0) {
            transform.Translate (Vector3.right * Input.GetAxis ("Mouse X") * Time.deltaTime);
		} else {
			if (transform.rotation != target.rotation) {
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