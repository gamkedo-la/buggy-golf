using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	public void PlayGame(string sceneName){
		SceneManager.LoadScene (sceneName);
    }

	public void QuitGame(){
		Application.Quit ();
	}
}

