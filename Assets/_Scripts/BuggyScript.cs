using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuggyScript : MonoBehaviour {

    // Motor and Steering of the wheels
    public List<Axle> axles;
    public float maxTorque;
    public float maxSteeringAngle;
    public string motorAxis;
    public string steeringAxis;

    // Boost Propulsion
    public ParticleSystem boostParticles;
    public float boostForce = 5000f;
    public string boostButton = "Boost";

    // UI
    public Slider speedometerSlider;

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