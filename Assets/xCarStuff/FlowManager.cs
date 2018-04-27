using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour {
	public float minY;
	void Start () {
		
	}
	
	void Update () {
		if (transform.position.y < minY)
		{
			GameObject.FindObjectOfType<Fading>().BeginFade(1);
			Invoke("ResetScene", 0.8f);
		}
	}

	void ResetScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void NextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
