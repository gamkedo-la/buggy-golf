using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobFollow : MonoBehaviour {
	Vector3 iniOffset;
	public Transform target;
	void Start () {
		iniOffset = target.position - transform.position;
	}
	
	void Update () {
		transform.position = target.position - iniOffset;
		transform.eulerAngles = new Vector3(90f, target.eulerAngles.y, target.eulerAngles.z);
	}
}
