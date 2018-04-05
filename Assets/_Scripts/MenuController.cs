﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	public void PlayGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }

	public void QuitGame(){
		Application.Quit ();
	}
}
