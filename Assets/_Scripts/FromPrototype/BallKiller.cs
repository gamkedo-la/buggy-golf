using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKiller : MonoBehaviour {

	public Collider collider;
	public Scoreboard scoreboard;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "Ball") {
			if (other.gameObject.GetComponent<BallScore> ().touched) {
				scoreboard.AddScore();
			}
			Destroy (other.gameObject);
		}
	}
}
