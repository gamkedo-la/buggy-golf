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
	public string outOfPlayName = "OutOfPlay";

    [Header("Inputs")]
    public string skipInput = "Jump";

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		// If ball has effectively stopped moving, end the stroke
		if (rb.velocity.magnitude < ballRestLimit && ballActive) { 
            StartCoroutine("EndStrokeCheck");
        }

		// Speed up time if skip button is pressed
        if (ballActive && canSpeedUpToSkip && Input.GetButton(skipInput)) {
            Time.timeScale = speedUpTimeScale;
        }
        else {
            Time.timeScale = 1f;
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        
		// See if ball is in the hole
		if (other.tag == holeTagName && ballActive)
        {
            holeManager.HoleEnd();
            Debug.Log("Ball in hole!");
            SoundManager.instance.winSong();
        }

		// See if ball is out of play
		if (other.tag == outOfPlayName && ballActive)
		{
			ballActive = false;
			BallEndStroke(true);
			Debug.Log("Ball out of play!");
		}
    }

    public void OnCollisionEnter(Collision collision)
    {
        SoundManager.instance.bumpSound();
    }

	public void BallEndStroke(bool mulligan) {
        rb.isKinematic = true; // Freeze the ball
		holeManager.StrokeReset(mulligan); // Reset play
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
            BallEndStroke(false);
        }
    }
}
