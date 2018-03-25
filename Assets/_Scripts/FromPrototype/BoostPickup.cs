using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPickup : MonoBehaviour {

	public CarBoost carBoost;
	public float cooldownTime = 3;

	private bool pickupActive = true;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other)
	{
		pickupActive = false;
		StartCoroutine (PickupCooldownTimer (cooldownTime));
		carBoost = other.gameObject.GetComponentInParent<CarBoost> ();
		carBoost.boostFuel = 100;
	}

	IEnumerator PickupCooldownTimer(float cooldown)
	{
		yield return new WaitForSeconds(cooldown);
		pickupActive = true;
	}
}
	