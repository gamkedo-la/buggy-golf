using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	public string groundColliderTag;
	public bool grounded = true;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other){
		if (other.tag == groundColliderTag) {
			grounded = true;
		} else
			grounded = false;
	}

	void OnTriggerExit (Collider other){
		if (other.tag == groundColliderTag) {
			grounded = false;
		}
	}
}
