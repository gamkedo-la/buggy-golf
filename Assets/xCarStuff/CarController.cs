using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriveType
{
	RWD,
	FWD,
	AWD
};
[System.Serializable]
public class WC
{
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
}
[System.Serializable]
public class WT
{
	public Transform wheelFL;
	public Transform wheelFR;
	public Transform wheelRL;
	public Transform wheelRR;
}
public class CarController : MonoBehaviour {
	public WC wheels;
	public WT tires;
	public DriveType DriveTrain = DriveType.RWD;
	public Vector3 centerOfGravity;//car's center of mass offset
	public float maxTorque = 1000f;//car's acceleration value
	public float maxReverseSpeed = 50f;//top speed for the reverse gear
	public float handBrakeTorque = 500f;//hand brake value
	public float maxSteer = 25f;//max steer angle

	private int throttleInput;//read only
	private int steerInput;//read only
	private bool reversing;//read only
	private float currentSpeed;//read only
	public float maxSpeed = 150f;//how fast the vehicle can go
	public float[] GearRatio;//determines how many gears the car has, and at what speed the car shifts to the appropriate gear
	private int gear;//current gear
	private Rigidbody rb;
	Vector3 localCurrentSpeed;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = centerOfGravity;
	}
	
	void FixedUpdate () {
		if (GetComponent<Rigidbody>().centerOfMass != centerOfGravity)
			GetComponent<Rigidbody>().centerOfMass = centerOfGravity;

		AllignWheels();
		Drive();
		EngineAudio();
	}

	void AllignWheels()
	{
		Quaternion quat;
		Vector3 pos;
		wheels.wheelFL.GetWorldPose(out pos, out quat);
		tires.wheelFL.position = pos;
		tires.wheelFL.rotation = quat;

		wheels.wheelFR.GetWorldPose(out pos, out quat);
		tires.wheelFR.position = pos;
		tires.wheelFR.rotation = quat;

		wheels.wheelRL.GetWorldPose(out pos, out quat);
		tires.wheelRL.position = pos;
		tires.wheelRL.rotation = quat;

		wheels.wheelRR.GetWorldPose(out pos, out quat);
		tires.wheelRR.position = pos;
		tires.wheelRR.rotation = quat;
	}

	void Drive()
	{
		float gasMultiplier = 0f;
		currentSpeed = rb.velocity.magnitude * 3.6f; //in km/h
		if (!reversing)
		{
			if (currentSpeed < maxSpeed)
				gasMultiplier = 1f;
			else
				gasMultiplier = 0f;

		}
		else
		{
			if (currentSpeed < maxReverseSpeed)
				gasMultiplier = 1f;
			else
				gasMultiplier = 0f;
		}

		if (DriveTrain == DriveType.RWD)
		{
			wheels.wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;
			wheels.wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;

			if (localCurrentSpeed.z < -0.1f && wheels.wheelRL.rpm < 10)
			{//in local space, if the car is travelling in the direction of the -z axis, (or in reverse), reversing will be true
				reversing = true;
			}
			else
			{
				reversing = false;
			}
		}
		if (DriveTrain == DriveType.FWD)
		{
			wheels.wheelFL.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;
			wheels.wheelFR.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;

			if (localCurrentSpeed.z < -0.1f && wheels.wheelFL.rpm < 10)
			{//in local space, if the car is travelling in the direction of the -z axis, (or in reverse), reversing will be true
				reversing = true;
			}
			else
			{
				reversing = false;
			}
		}
		if (DriveTrain == DriveType.AWD)
		{

			wheels.wheelFL.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;
			wheels.wheelFR.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;
			wheels.wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;
			wheels.wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical") * gasMultiplier;

			if (localCurrentSpeed.z < -0.1f && wheels.wheelRL.rpm < 10)
			{//in local space, if the car is travelling in the direction of the -z axis, (or in reverse), reversing will be true
				reversing = true;
			}
			else
			{
				reversing = false;
			}
		}

		wheels.wheelFL.steerAngle = maxSteer * Input.GetAxis("Horizontal");
		wheels.wheelFR.steerAngle = maxSteer * Input.GetAxis("Horizontal");
		if (Input.GetButton("Break"))//pressing space triggers the car's handbrake
		{
			wheels.wheelFL.brakeTorque = handBrakeTorque;
			wheels.wheelFR.brakeTorque = handBrakeTorque;
			wheels.wheelRL.brakeTorque = handBrakeTorque;
			wheels.wheelRR.brakeTorque = handBrakeTorque;
		}
		else//letting go of space disables the handbrake
		{
			wheels.wheelFL.brakeTorque = 0f;
			wheels.wheelFR.brakeTorque = 0f;
			wheels.wheelRL.brakeTorque = 0f;
			wheels.wheelRR.brakeTorque = 0f;
		}
	}

	void EngineAudio()
	{
		//Engine sound + gearing sound (managed with pitch)
		for (int i = 0; i < GearRatio.Length; i++)
		{
			if (GearRatio[i] > currentSpeed)
			{
				//break this value
				break;
			}

			float minGearValue = 0f;
			float maxGearValue = 0f;
			if (i == 0)
			{
				minGearValue = 0f;
			}
			else
			{
				minGearValue = GearRatio[i];
			}
			maxGearValue = GearRatio[i + 1];

			float pitch = ((currentSpeed - minGearValue) / (maxGearValue - minGearValue) + 0.3f * (gear + 1));
			GetComponent<AudioSource>().pitch = pitch;

			gear = i;
		}
	}

}
