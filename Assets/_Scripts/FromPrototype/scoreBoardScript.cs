using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreBoardScript : MonoBehaviour {

	public Text scoreField;
	public int score;

	// Use this for initialization
	void Start () {

		scoreField.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
