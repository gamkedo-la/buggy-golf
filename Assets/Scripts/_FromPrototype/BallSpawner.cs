using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

	public GameObject ball;
	public bool ballReady = true;
	public float ballCooldown =3f;
	public float ballOffsetZ =10;
	public string ballSpawnButton = "BallSpawn";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton (ballSpawnButton)) {
			if (ballReady) {
				Vector3 spawnPosition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z + ballOffsetZ));
				Instantiate (ball, spawnPosition, gameObject.transform.rotation);
				ballReady = false;
				StartCoroutine (BallCooldownTimer (ballCooldown));
			}
		
		}
	}

	IEnumerator BallCooldownTimer(float cooldown){
		yield return new WaitForSeconds(cooldown);
		ballReady = true;
	}

}
