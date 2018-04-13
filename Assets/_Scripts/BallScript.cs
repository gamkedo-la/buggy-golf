using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public HoleManager holeManager;
    public float ballRestLimit = 0.5f; // How little should the ball move before we say it is still?
    public bool ballActive = false; // Has the ball been hit?

    [Header("Car Reset")]
    public CarResetter carResetter;
    public CarResetterCage carResetterCage;

    Rigidbody rb;
       

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (!carResetter) {
            GetComponentInChildren<CarResetter>();
        }
        if (!carResetterCage){
            GetComponentInChildren<CarResetterCage>();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity.magnitude < ballRestLimit && ballActive) { // If ball has effectively stopped moving, end the stroke
            EndStroke();
            ballActive = false;
        }
	}

    public void EndStroke() {
        rb.isKinematic = true; // Freeze the ball
        holeManager.StrokeReset(); // Reset play
        Debug.Log("Stroke is over!");
    }
}
