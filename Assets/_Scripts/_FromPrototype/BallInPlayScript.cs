using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInPlayScript : MonoBehaviour {

    public HoleManager holeManager;

    [Header("Collisions")]
    public Collider[] carColliders;
    public string ballTag;

    [Header("Buggy")]
	private GameObject models;
    private BuggyScript buggyScript;
    public float buggyDeactivateDelay;
	private ClubManager clubManager;
    private GameObject playerGO;
    private GameObject clubsGO;

   
    void Start() {
        // Populate our Player, Club, modelm and camera variables Variables
        holeManager = GameObject.FindGameObjectWithTag("HoleManager").GetComponent<HoleManager>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
		buggyScript = playerGO.GetComponent<BuggyScript>();
		clubsGO = GameObject.FindGameObjectWithTag("ClubManager");
		clubManager = clubsGO.GetComponent<ClubManager>();
		models = playerGO.transform.Find("Models").gameObject;
    }    


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == ballTag) {
            // Switch from the car camera to the ball camera
            holeManager.cameraManager.CameraBallHitSwitch();
                        
            // Activate the ball for stroke tracking
            BallScript bs = other.gameObject.GetComponent<BallScript>();
            bs.ballActive = true;

            StartCoroutine(BuggyDisable(buggyDeactivateDelay)); // Disable the buggy so it doesn't keep hitting the ball       
        }
    }


    public void PositionBuggy(Vector3 buggyPosition) {
        Transform buggyTrans = buggyScript.gameObject.transform;
        buggyTrans.position = buggyPosition;
        buggyScript.buggyRb.position = buggyTrans.position;
        buggyTrans.LookAt(holeManager.ball.gameObject.transform);        
    }

    public void BuggyEnable() { // Enable buggy (for stroke reset)
        models.SetActive(true);
        buggyScript.enabled = true; // Return control of Buggy
        buggyScript.buggyRb.isKinematic = false;
        clubManager.SetClub(0);
        
    }


    IEnumerator BuggyDisable(float delay) { // Disable the Buggy after ball contact
        buggyScript.buggyRb.isKinematic = true; //turn off the rigidbody
        buggyScript.enabled = false; // Remove control of Buggy
        //TODO make this disable club colliders but leave the models for aethetics. Need to re-enable them when we reset stroke.
        clubManager.DisableAllClubs(); // Turn off clubs and their collisions
        yield return new WaitForSeconds(delay);
        models.SetActive(false); // Make the car disappear to avoid weirdness
        
    }
}
