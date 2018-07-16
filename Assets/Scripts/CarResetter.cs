using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarResetter : MonoBehaviour {
    
    public bool resettingCar = false; // Are we pickign where the car goes next right now?
    public GameObject carPlacer; // Placement representation to move around
    public Camera resetterCamera;
    public CarAcrobatics player;

    public float placementSpeed = 1; // Placement cursor speed

    [Header("Images")]
    public Image limitAreaTip;
    public SpriteRenderer carRepresentationImage;

    [Header("Ranges")]
    public float rangeXMax = 0f;
    public float rangeXMin = 0f;
    public float rangeZMax = 0f;
    public float rangeZMin = 0f;

    [Header("Inputs")]
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Tilt";
    public string confirmInput = "Submit";
    public string rayTag = "Ground";

    private void Start()
    {
        limitAreaTip = GetComponent<Image>();
        limitAreaTip.enabled = false;
        carRepresentationImage.enabled = false;
        resetterCamera = GameObject.FindGameObjectWithTag("CameraPlayReset").GetComponent<Camera>();
    }

    void Update() {
        if (resettingCar) {
            if (Input.GetAxis(horizontalInput) != 0) {
                carPlacer.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * placementSpeed,Space.World);
                ClampPlacerPosition();
            }
            if (Input.GetAxis(verticalInput) != 0) {
                carPlacer.transform.Translate(Vector3.forward * Input.GetAxis("Tilt") * placementSpeed,Space.World);
                ClampPlacerPosition();
            }

            Vector3 centerPos = Vector3.zero;
            centerPos.x = (rangeXMin+rangeXMax)*0.5f;
            centerPos.z = (rangeZMin+rangeZMax)* 0.5f;
            float faceBall = Mathf.Atan2(centerPos.x - carPlacer.transform.localPosition.x,
                                         centerPos.z - carPlacer.transform.localPosition.z);
            carPlacer.transform.rotation = Quaternion.AngleAxis(faceBall*Mathf.Rad2Deg , Vector3.up) *
                Quaternion.AngleAxis(90.0f, Vector3.right);
            if (Input.GetButtonDown(confirmInput)) { // On select, raycast and check for ground, then set the car there
                RaycastHit hit;
                Ray ray = new Ray(carPlacer.transform.position, (transform.up*(-1)));
                Debug.Log("DEBUG: ray is " + ray);
                //Debug.DrawRay(ray.origin, ray.direction, Color.green, 5f);
                if (Physics.Raycast(ray, out hit)) {
                    Debug.Log("DEBUG: hit is " + hit.transform.name);
                    Transform objectHit = hit.transform;
                    if (objectHit.tag == rayTag) { // Check if we are on the ground, and then spawn the buggy
                        ResetterSetBuggy(hit.point);
                        ResetterEndReset();
                        HoleManager.Instance.HolePlayStart();
                    }               
                }
            }
        }
    }

    public void ResetterSetBuggy(Vector3 setPosition) {
        HoleManager.Instance.player.ballInPlayScript.PositionBuggy(setPosition);
        HoleManager.Instance.player.ballInPlayScript.BuggyEnable();
        resettingCar = false;
    }

    void ClampPlacerPosition() {
        GameObject ball = HoleManager.Instance.ball.gameObject;
        Vector3 placerPosition = carPlacer.transform.localPosition;
        float placerX = (Mathf.Clamp(placerPosition.x, rangeXMin, rangeXMax));
        float placerZ = (Mathf.Clamp(placerPosition.z, rangeZMin, rangeZMax));
        carPlacer.transform.localPosition = new Vector3(placerX, carPlacer.transform.localPosition.y, placerZ);
    }

    public void ResetterStartReset() {
        resettingCar = true;
        limitAreaTip.enabled = true;
        carRepresentationImage.enabled = true;
    }

    public void ResetterEndReset() {
        resettingCar = false;
        limitAreaTip.enabled = false;
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

