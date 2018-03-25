using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.gameObject.transform.LookAt(target, Vector3.up);
		this.transform.localRotation.Set (0, this.transform.rotation.y, 0, transform.rotation.w);
	}
}
