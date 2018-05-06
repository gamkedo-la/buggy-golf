using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClubManager : MonoBehaviour {

    public List<ClubScript> clubs; // Our list of clubs
    public string clubButton;
    UIManager uIManager;

    // Keep track of what club we're on
    int clubCount;
    int activeClub = 0;
    int newClub;

    // Handle UI switch
    public Image clubUI;


    // TODO - Make clubs autopopulate from children
    void Start () {
        uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        clubUI = uIManager.clubIcon.GetComponent<Image>();
        clubCount = clubs.Count; // How many clubs are in the list?
        for (int i = 0; i < clubs.Count; i++) {
            clubs[i].gameObject.SetActive(false); // Deactivate all clubs in case we forgot to elsewhere
        }
        SetClub(0); // Activate the first club
	}
	
	void FixedUpdate () {
        if (Input.GetButtonDown(clubButton)){
            newClub = activeClub;
            newClub++;
            if (newClub > (clubCount-1)) { // Make sure we have that club
                newClub = 0;
            }
            ChangeClub(newClub, activeClub); // Change the club
        }
	}

    public void ChangeClub (int activateClub, int deactivateClub) {
        clubs[activateClub].gameObject.SetActive(true); // Activate the new club
        clubs[deactivateClub].gameObject.SetActive(false); // Deactivate the old club
        activeClub = activateClub;
        clubUI.sprite = clubs[activeClub].clubSprite;
    }

    public void SetClub(int activateClub) { // We only activate a club
        clubs[activateClub].gameObject.SetActive(true); // Activate the new club
        activeClub = activateClub;
        clubUI.sprite = clubs[activeClub].clubSprite;
    }

    public void DisableAllClubs() {
        for (int i = 0; i < clubs.Count; i++)
        {
            clubs[i].gameObject.SetActive(false); // Deactivate all clubs
        }
    }
}
