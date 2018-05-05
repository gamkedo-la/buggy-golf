using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeScript : MonoBehaviour {

    public GameObject holeDetection;
    public HoleManager holeManager;


	// Use this for initialization
	void Start () {
		holeManager = GameObject.FindGameObjectWithTag("HoleManager").GetComponent<HoleManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
