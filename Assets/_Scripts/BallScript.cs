using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    public HoleManager holeManager;
    public float ballRestLimit = 0.5f; // How little should the ball move before we say it is still?
    public float timeBuffer = 1f; // Preventing instant stroke end at walls, etc.
    public bool ballActive = false; // Has the ball been hit?
    public bool canSpeedUpToSkip = true; // Can speed up the ball roll to skip the scene (doesn't affect physics, for the impatient)?
    public float speedUpTimeScale = 5f;
    public string holeTagName = "Hole Detector";

    [Header("Inputs")]
    public string skipInput = "Jump";

    Rigidbody rb;       

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity.magnitude < ballRestLimit && ballActive) { // If ball has effectively stopped moving, end the stroke
            StartCoroutine("EndStrokeCheck");
            
        }

        if (ballActive && canSpeedUpToSkip && Input.GetButton(skipInput)) {
            Time.timeScale = speedUpTimeScale;
        }
        else {
            Time.timeScale = 1f;
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == holeTagName && ballActive)
        {
            holeManager.HoleEnd();
            Debug.Log("Ball in hole!");
        }
    }

    public void BallEndStroke() {
        rb.isKinematic = true; // Freeze the ball
        holeManager.StrokeReset(); // Reset play
        Debug.Log("Stroke is over!");
    }

    public void BallStartStroke() {
        rb.isKinematic = false;
    }

    IEnumerator EndStrokeCheck() {
        yield return new WaitForSeconds(timeBuffer);
        if (rb.velocity.magnitude < ballRestLimit && ballActive)
        {
            ballActive = false;
            BallEndStroke();
        }
    }
}
