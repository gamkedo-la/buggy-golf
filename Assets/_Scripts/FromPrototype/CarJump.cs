using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class CarJump : MonoBehaviour {

	public GroundCheck groundCheck;
	public bool isGrounded;
	public string jumpButton = "Jump";
	Rigidbody rb;
	public float jumpForce;
	public bool jumping;
	public float cooldownTime;
	//Vector3 thrust;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		//thrust = new Vector3(0,jumpForce,0);
	}
	// Update is called once per frame
	void Update () {
		isGrounded = groundCheck.grounded;

		if (Input.GetButton (jumpButton)) {
			if (isGrounded && jumping == false) {
				rb.AddRelativeForce (Vector3.up * jumpForce, ForceMode.VelocityChange);
				jumping = true;
				//
			}
		}

		if (isGrounded && jumping == true) {
			StartCoroutine (JumpCooldown (cooldownTime));
		}
	}

	IEnumerator JumpCooldown (float time){
		yield return new WaitForSeconds (time);
		jumping = false;
	}
}
