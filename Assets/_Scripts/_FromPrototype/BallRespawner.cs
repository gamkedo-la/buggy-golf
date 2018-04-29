using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : MonoBehaviour {

	public string tagOfBall;
	public GameObject ballPrefab;
	public Transform respawnLocation;

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == tagOfBall)
		{
			Destroy(col.gameObject);
			Instantiate (ballPrefab, respawnLocation.transform.position, respawnLocation.transform.rotation);
		}
	}
}
