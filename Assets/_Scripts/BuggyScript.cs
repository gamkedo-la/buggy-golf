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
    public float angularDrag = 1.0f;

    [Header("Boost")]
    // Boost Propulsion
    public ParticleSystem boostParticles;
    public int boostParticleCount;
    public float boostForce = 5000f;
    public string boostButton = "Boost";
    public float accelForce = 5000f;
    public Vector3 accelForceReadout;
    // public string boostButton = "Boost";

    [Header("UI")]
    // UI
    public Slider speedometerSlider;

    [Header("Center of Mass")]
    // Center of Mass
    public Vector3 readCenterOfMass;  //
    public Vector3 newCenterOfMass;
    public Rigidbody buggyRb;
    public bool adjustCenterOfMass;

    [Header("Reset and Contact")]
    public BallInPlayScript ballInPlayScript;

    public void Start()
    {
        // Read center of mass calculated by Unity. If we choose, change it.
        buggyRb = GetComponent<Rigidbody>();
        readCenterOfMass = buggyRb.centerOfMass;
        buggyRb.angularDrag = angularDrag;

        if (adjustCenterOfMass){
            buggyRb.centerOfMass = newCenterOfMass;
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
            buggyRb.AddRelativeForce(Vector3.forward * boostForce, ForceMode.Force);
            boostParticles.Emit(boostParticleCount);
        }

        if (Input.GetAxis(motorAxis) != 0){
            accelForceReadout = Vector3.forward * accelForce * Input.GetAxis(motorAxis);
            buggyRb.AddRelativeForce(accelForceReadout, ForceMode.Force);
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