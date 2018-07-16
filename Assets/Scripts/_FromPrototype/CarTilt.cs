using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTilt : MonoBehaviour {

	public CarJump carJump;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (carJump.isGrounded == false) {
			transform.Rotate (Input.GetAxis ("Tilt"), Input.GetAxis ("Horizontal"), 0.0f );

		}
		
	}
}
