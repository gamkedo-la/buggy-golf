using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorecardUIScript : MonoBehaviour {

    public Text par;
    public Text gameScore;
    public Text holeScore;
	public Text holeName;
    public Canvas canvas;

    
    
    // Use this for initialization
    void Start () {
        canvas = this.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
}
