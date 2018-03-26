using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuggyScript : MonoBehaviour {

    [Header("Motor and Steering")]
    // Motor and Steering of the wheels
    public List<Axle> axles;
    public float maxTorque;
    public float maxSteeringAngle;
    public string motorAxis;
    public string steeringAxis;

    [Header("Boost")]
    // Boost Propulsion
    public ParticleSystem boostParticles;
    public int boostParticleCount;
    public float boostForce = 5000f;
    public string boostButton = "Boost";

    [Header("UI")]
    // UI
    public Slider speedometerSlider;

    [Header("Center of Mass")]
    // Center of Mass
    public Vector3 readCenterOfMass;  //
    public Vector3 newCenterOfMass;
    Rigidbody thisRb;
    public bool adjustCenterOfMass;


    public void Start()
    {
        // Read center of mass calculated by Unity. If we choose, change it.
        thisRb = GetComponent<Rigidbody>();
        readCenterOfMass = thisRb.centerOfMass;
        if (adjustCenterOfMass){
            thisRb.centerOfMass = newCenterOfMass;
        }
    }


    public void FixedUpdate() {

        // Handle Motor and Steering of the wheels
        float motor = maxTorque * Input.GetAxis(motorAxis);
        float steering = maxSteeringAngle * Input.GetAxis(steeringAxis);
        foreach (Axle axle in axles) {
            if (axle.steering) {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }
            if (axle.steering) {
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
            }
        }

        // Handle Boosting
        if (Input.GetButton(boostButton)) {
            thisRb.AddRelativeForce(Vector3.forward * boostForce, ForceMode.Force);
            boostParticles.Emit(boostParticleCount);
        }
    }
}

[System.Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // are these motor wheels?
    public bool steering; // are these steering wheels?
}