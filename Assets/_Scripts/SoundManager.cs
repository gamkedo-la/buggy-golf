using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip rev;
	public AudioClip boost;
	public AudioClip boostTailLoop;

    public AudioClip hitSound;
    public AudioClip bounceSound;
    public AudioClip goalSound;

    private AudioSource revSource;
    private AudioSource boostSource;
    private AudioSource hitSource;
    private AudioSource bumpSource;
    private AudioSource goalSource;

    public static SoundManager instance;

    // Use this for initialization
    void Start () {
        instance = this;
        revSource = this.GetComponent<AudioSource>();
        boostSource = gameObject.AddComponent<AudioSource>();
        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.clip = hitSound;
        bumpSource = gameObject.AddComponent<AudioSource>();
        bumpSource.clip = bounceSound;
        goalSource = gameObject.AddComponent<AudioSource>();
        goalSource.clip = goalSound;

        revSource.clip = rev;
        revSource.loop = true;
        revSource.Play();
    }

    // Update is called once per frame
    void Update () {
        revSource.volume = Mathf.Abs(Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayBoostOneOff();
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayBoostTailLoop();
        }

    }

    public void bangSound()
    {
        hitSource.Play();
    }
    public void bumpSound()
    {
        bumpSource.Play();
    }

    public void winSong()
    {
        if (goalSource.isPlaying == false)
        {
            goalSource.Play();
        }
    }

    void PlayBoostOneOff()
	{
        boostSource.clip = boost;
        boostSource.loop = true;
        boostSource.Play();

	}
	void PlayBoostTailLoop()
	{
        boostSource.clip = boostTailLoop;
        boostSource.loop = false;
        boostSource.Play();

	}
}
