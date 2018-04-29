using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarBoost : MonoBehaviour {

	public CarJump carJump;
	public string boostButton = "Boost";
	public float airBoostForce = 12500f;
	public float groundBoostForce = 7500f;
	public Rigidbody rb;
	public float boostFuel = 100f;
	public float boostCost = 20f;
	//public Text boostMeter;
	public bool IsBoosting = false;
	public Slider boostSlider;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		//boostMeter.text = (Mathf.Round(boostFuel)).ToString ();

	}

	void Update ()
	{
	//	boostSlider.value = boostFuel;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetButton(boostButton))
        {
            if (carJump.isGrounded == false && boostFuel > 0)
            {
                rb.AddRelativeForce(Vector3.forward * airBoostForce, ForceMode.Force);
                boostFuel -= (boostCost * Time.deltaTime);
                //boostMeter.text = (Mathf.Round(boostFuel)).ToString ();
                Debug.Log("Air Boosting!");
                IsBoosting = true;


            }
            if (carJump.isGrounded == true && boostFuel > 0)
            {
                rb.AddRelativeForce(Vector3.forward * groundBoostForce, ForceMode.Force);
                boostFuel -= (boostCost * Time.deltaTime);
                //boostMeter.text = (Mathf.Round(boostFuel)).ToString ();
                Debug.Log("Ground Boosting!");
                IsBoosting = true;

                //rb.AddRelativeForce ((0,0,1) * boostForce, ForceMode.Force);
            }
        }
        else IsBoosting = false;
	}
}