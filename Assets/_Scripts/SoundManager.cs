using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip rev;
	public AudioClip boost;

	public AudioClip boostTailLoop;

	public AudioSource mySource;

	// Use this for initialization
	void Start () {
		mySource = this.GetComponent<AudioSource>();
		PlayRevLoop();
		
	}
	
	// Update is called once per frame
	void Update () {

		
		
	}

	void PlayRevLoop()
	{
		mySource.clip = rev;
		mySource.loop = true;
		mySource.Play();

	}

	void PlayBoostOneOff()
	{
		mySource.clip = boost;
		mySource.loop = false;
		mySource.Play();

	}
	void PlayBoostTailLoop()
	{
		mySource.clip = boostTailLoop;
		mySource.loop = true;
		mySource.Play();

	}
}
