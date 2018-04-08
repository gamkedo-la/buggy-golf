using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public HoleManager holeManager;
    public float ballRestLimit = 0.5f; // How little should the ball move before we say it is still?
    public bool ballActive = false; // Has the ball been hit?

    Rigidbody rb;
       

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity.magnitude < ballRestLimit && ballActive) { // If ball has effectively stopped moving, end the stroke
            EndStroke();
        }
	}

    public void EndStroke() {
        holeManager.strokeOver = true;
        Debug.Log("Stroke is over!");
    }
}
