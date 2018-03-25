using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetSwitcher : MonoBehaviour {

	public GameObject[] targets;
	public string switchButton;
	public LookAtScript lookAtScript;
	public int activeTarget = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown (switchButton))
			{
			activeTarget++;
			if (activeTarget > (targets.Length-1))
			{
				activeTarget = 0;
			}
			lookAtScript.target = targets [activeTarget].transform;
			}
	}
}
