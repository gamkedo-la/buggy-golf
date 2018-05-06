using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarResetter : MonoBehaviour {

    public HoleManager holeManager;
    public bool resettingCar = false; // Are we pickign where the car goes next right now?
    public GameObject carPlacer; // Placement representation to move around
    public Camera resetterCamera;

    public float placementSpeed = 1; // Placement cursor speed

    [Header("Images")]
    public Image limitAreaImage;
    public SpriteRenderer carRepresentationImage;

    [Header("Ranges")]
    public float rangeXMax = 0f;
    public float rangeXMin = 0f;
    public float rangeZMax = 0f;
    public float rangeZMin = 0f;

    [Header("Inputs")]
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Tilt";
    public string confirmInput = "Jump";
    public string rayTag = "Ground";

    private void Start() {
        limitAreaImage.enabled = false;
        carRepresentationImage.enabled = false;
        resetterCamera = GameObject.FindGameObjectWithTag("CameraPlayReset").GetComponent<Camera>();
    }

    void FixedUpdate() {
        if (resettingCar) {
            if (Input.GetAxis(horizontalInput) != 0) {
                carPlacer.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * placementSpeed);
                ClampPlacerPosition();
            }
            if (Input.GetAxis(verticalInput) != 0) {
                carPlacer.transform.Translate(Vector3.up * Input.GetAxis("Tilt") * placementSpeed);
                ClampPlacerPosition();
            }
            if (Input.GetButtonDown(confirmInput)) { // On select, raycast and check for ground, then set the car there
                RaycastHit hit;
                Ray ray = new Ray(carPlacer.transform.position, (transform.up*(-1)));
                Debug.Log(ray);
                //Debug.DrawRay(ray.origin, ray.direction, Color.green, 5f);
                if (Physics.Raycast(ray, out hit)) {
                    Transform objectHit = hit.transform;
                    if (objectHit.tag == rayTag) { // Check if we are on the ground, and then spawn the buggy
                        ResetterSetBuggy(hit.point);
                        ResetterEndReset();
                        holeManager.HolePlayStart();
                    }               
                }
            }
        }
    }

    public void ResetterSetBuggy(Vector3 setPosition) {
        holeManager.player.ballInPlayScript.PositionBuggy(setPosition);
        holeManager.player.ballInPlayScript.BuggyEnable();
        resettingCar = false;
    }


    void ClampPlacerPosition() {
        GameObject ball = holeManager.ball.gameObject;
        Vector3 placerPosition = carPlacer.transform.localPosition;
        float placerX = (Mathf.Clamp(placerPosition.x, rangeXMin, rangeXMax));
        float placerZ = (Mathf.Clamp(placerPosition.z, rangeZMin, rangeZMax));
        carPlacer.transform.localPosition = new Vector3(placerX, carPlacer.transform.localPosition.y, placerZ);
    }

    public void ResetterStartReset() {
        resettingCar = true;
        limitAreaImage.enabled = true;
        carRepresentationImage.enabled = true;
    }

    public void ResetterEndReset() {
        resettingCar = false;
        limitAreaImage.enabled = false;
        carRepresentationImage.enabled = false;
    }

}













    // FIRST ATTEMPT, DIDN"T WORK
    /*public bool resettingCar = false;
    public GameObject ball;
    MeshRenderer meshRenderer;


    public void ResetterStartReset() {
        resettingCar = true;
        this.transform.position = ball.transform.position;
       // this.meshRenderer.enabled = true;
    }
    
    // Use this for initialization
	void Start () {
        //resettingCar = false;
        //this.meshRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (resettingCar) {
            if (Input.GetAxis("Horizontal") != 0)
            {
                transform.Translate(Vector3.right * Input.GetAxis("Horizontal"));
            }
            if (Input.GetAxis("Tilt") != 0)
            {
                transform.Translate(Vector3.forward * Input.GetAxis("Tilt"));
            }
            //this.transform.LookAt(ball.transform);
        }

	}*/

