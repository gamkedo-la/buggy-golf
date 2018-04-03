using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInPlayScript : MonoBehaviour {

    [Header("Collisions")]
    public Collider[] carColliders;
    public string ballTag;

    [Header("Cameras")]
    public Camera carCam;
    public Camera ballCam;

    [Header("Buggy")]
    public GameObject models;
    public BuggyScript buggyScript;
    public float buggyDeactivateDelay;
    public ClubManager clubManager;

    // Use this for initialization
    void Start() {

    }    

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == ballTag) {
            // Switch from the car camera to the ball camera
            ballCam.transform.position = carCam.transform.position; // Put cameras in the same place
            ballCam.gameObject.SetActive(true);
            carCam.gameObject.SetActive(false);

            // Activate the ball for stroke tracking
            BallScript bs = other.gameObject.GetComponent<BallScript>();
            bs.ballActive = true;

            StartCoroutine(BuggyDisable(buggyDeactivateDelay));            
        }
    }

    public void BuggyEnable() { // Enable buggy (for stroke reset)
        models.SetActive(true);
        buggyScript.enabled = true; // Return control of Buggy
        clubManager.SetClub(0);     
    }

    IEnumerator BuggyDisable(float delay) { // Disable the Buggy after ball contact
        buggyScript.enabled = false; // Remove control of Buggy
        //TODO make this disable club colliders but leave the models for aethetics. Need to re-enable them when we reset stroke.
        clubManager.DisableAllClubs(); // Turn off clubs and their collisions
        yield return new WaitForSeconds(delay);
        models.SetActive(false); // Make the car disappear to avoid weirdness
    }
}
