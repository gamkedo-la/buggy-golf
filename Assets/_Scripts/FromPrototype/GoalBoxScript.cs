using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBoxScript : MonoBehaviour {

	public int teamScore = 0;
	public Text scoreDisplay;
	public string tagOfBall;
	public bool notBall =false;

	public void OnTriggerEnter (Collider col)
	{
		if (col.tag != tagOfBall) {
			notBall = true;
			return;
		}
		teamScore++;
		scoreDisplay.text = teamScore.ToString ();

	}
}
