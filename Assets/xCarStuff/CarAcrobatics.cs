﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using EZCameraShake;
using UnityEngine.UI;
using TMPro;

//This class is a big mess, sorry. This was made during the end of ludum dare 41
public class CarAcrobatics : MonoBehaviour {
	public WC wheels; //Class containing all wheel colliders
	[SerializeField]
	float fastTorque, jumpForce, jumpForce2, jumpForce3, multHelpTorque, multFloorTorque, barrelTorqueM;
	private Rigidbody rb;
    [HideInInspector]
    public Rigidbody buggyRb;
	
    private float iX, iY; //We'll store input here
	private float mult = 1f; 
	private bool upsideD = false; //Tells if car is upside down or not
	private bool isBoost = false; //Tells if car is boosting or not
	private bool boostActive = false; //The same thing, it should be refactored
    private bool hasDJump = false;
    private bool grounded = false;

    private bool holdForReset = false; //Delay bringing controls online after respawn for stillness
    private float holdForResetDelayTime = .25f;

	public ParticleSystem boost; //Bosting particle system
	private ParticleSystem.EmissionModule em;
    public CarController carCon;
	public VehicleCameraControl vc;
	public PostProcessingProfile postProcessing;
	private CameraShakeInstance ci;
    public bool useCameraShake = false;
	public float magnitude, rough, fadeIn, fadeOut; //Properties of the boost camera shake
	
	public float extraTorque; 
	public Image boostI; //Ui that inidicates how much boost we have left
	[SerializeField]
	float fallMult, lowJumpMult, jumpYSpeed, boostSpeed, boostAmount, boostConsume, boostFill;
	private float currentBoost;

    // GAME MANAGER STUFF
    [HideInInspector]
    public BallInPlayScript ballInPlayScript;


	void Awake()
	{
		Cursor.visible = false;
		vc = GameObject.FindObjectOfType<VehicleCameraControl>();
	}

	void Start () {
		rb = GetComponent<Rigidbody>();
        buggyRb = rb;
        carCon = GetComponent<CarController>();
		GetComponentInChildren<BlobFollow>().transform.parent = null;
		em = boost.emission;
		em.enabled = false;
		currentBoost = boostAmount;

        //GAME MANAGER STUFF
        ballInPlayScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BallInPlayScript>();
	}

	void FixedUpdate () {
        if (!UIManager.Instance.carResetter.resettingCar && !holdForReset)
        {
            iX = Input.GetAxis("Horizontal") * 0.5f;
            iY = Input.GetAxis("Vertical");
            grounded = IsGrounded();
            if (!grounded)
            {
                if (upsideD)
                {
                    mult *= multFloorTorque;
                    rb.AddRelativeTorque(-iY * fastTorque * mult, 0.0f, -iX * fastTorque * mult);
                }
                else
                {
                    AirTorque();
                    JumpGameFeel();
                }
            }

            if (rb.angularVelocity.magnitude > 4.0f || rb.angularVelocity.magnitude < 2f)
            {
                mult = multHelpTorque;
            }
            else mult = 1f;
            rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 5f);

            HandleBoost();
        }
        else
        {
            // If we're resetting, kill boost and jump

        }

	}

	void Update()
	{
        if (!UIManager.Instance.carResetter.resettingCar && !holdForReset)
        {
            if (Input.GetButtonDown("Boost2"))
            {
                isBoost = true;
            }
            else if (Input.GetButtonUp("Boost2") || currentBoost <= 0f || UIManager.Instance.carResetter.resettingCar)
            {
                isBoost = false;
            }
            else
            {
                if (Input.GetButtonDown("Jump") && grounded)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                }
            }
        }
	}

	private void AirTorque()
	{
		if (Input.GetButton("Roll"))
		{
			rb.AddRelativeTorque(iY * fastTorque * mult * extraTorque, 0.0f, -iX * fastTorque * mult * barrelTorqueM * extraTorque);
		}
		else
		{
			rb.AddRelativeTorque(iY * fastTorque * mult, iX * fastTorque * mult, 0.0f);
		}
	}

	private void JumpGameFeel()
	{
		//Jump "Game-feel" improvement
		if (rb.velocity.y < jumpYSpeed) //If we are falling
		{
			//We apply more force downwards to fall faster
			rb.velocity -= Vector3.down * Physics.gravity.y * fallMult * Time.deltaTime;
		}
		else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) //If we are going up and not pressing the jump button
		{
			//We apply more force downwards to fall faster
			rb.velocity -= Vector3.down * Physics.gravity.y * lowJumpMult * Time.deltaTime;
		}
	}

	bool IsGrounded()
	{
		bool g = (wheels.wheelFL.isGrounded || wheels.wheelFR.isGrounded || wheels.wheelRL.isGrounded || wheels.wheelRR.isGrounded);
		return g;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Untagged")
		{
			upsideD = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Untagged")
		{
			upsideD = false;
		}
	}

	void HandleBoost()
	{
		//Debug.Log(rb.velocity.magnitude);
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, 82f);
        if (isBoost)
		{
			if (currentBoost > 0f)
			{
				if (rb.velocity.magnitude <= 0.1f)
				{
					rb.AddForce(transform.forward * 30000f, ForceMode.Impulse);
				}
				rb.AddForce(transform.forward * boostSpeed, ForceMode.Acceleration);
				currentBoost -= boostConsume;
				//boostI.fillAmount = currentBoost / boostAmount;
			}
			if (!boostActive)
			{
				em.enabled = true;
				boostActive = true;
				postProcessing.chromaticAberration.enabled = true;
                if(useCameraShake)
                {
                    ci = CameraShaker.Instance.StartShake(magnitude, rough, fadeIn);    
                }
				WheelFrictionCurve frictionCurve = wheels.wheelFL.sidewaysFriction;
				frictionCurve.stiffness = 30f;
				frictionCurve = wheels.wheelFR.sidewaysFriction;
				frictionCurve.stiffness = 30f;
				frictionCurve = wheels.wheelRL.sidewaysFriction;
				frictionCurve.stiffness = 30f;
				frictionCurve = wheels.wheelRR.sidewaysFriction;
				frictionCurve.stiffness = 30f;
			}
		}
		else if (!isBoost && boostActive)
		{
			em.enabled = false;
			boostActive = false;
			postProcessing.chromaticAberration.enabled = false;
            if(ci != null)
            {
                ci.StartFadeOut(fadeOut);    
            }
			WheelFrictionCurve frictionCurve = wheels.wheelFL.sidewaysFriction;
			frictionCurve.stiffness = 12f;
			frictionCurve = wheels.wheelFR.sidewaysFriction;
			frictionCurve.stiffness = 12f;
			frictionCurve = wheels.wheelRL.sidewaysFriction;
			frictionCurve.stiffness = 12f;
			frictionCurve = wheels.wheelRR.sidewaysFriction;
			frictionCurve.stiffness = 12f;
		}
		else if (!isBoost && grounded)
		{
			if (currentBoost < boostAmount)
			{
				currentBoost += boostFill;
				//boostI.fillAmount = currentBoost / boostAmount;
			}
		}
	}

    public void StopWheels()
    {
        carCon.StopWheels(); 
    }

    public void KillBoostJump()
    {
        isBoost = false;
        grounded = false;
    }

    public void BuggyResetDelay()
    {
        holdForReset = true;
        StartCoroutine("BuggyResetControlDelay");
    }

    public void BuggyEnable()
    {
        rb.isKinematic = false;
        StopWheels();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;    
    }

    IEnumerator BuggyResetControlDelay()
    {
        yield return new WaitForSeconds(holdForResetDelayTime);

    }
}
