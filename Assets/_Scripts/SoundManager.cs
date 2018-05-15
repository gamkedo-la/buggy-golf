using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public bool isMainMenuJustForVolSet = false;

    public AudioSource musicSource;

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
    private bool isCarMuted = false;

    public static SoundManager instance;

    public static float masterVol = 0.7f;
    public static float musicVol = 0.7f;
    public static float soundVol = 0.7f;

    // Use this for initialization
    void Start () {
        instance = this;

        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.clip = hitSound;

        updateVolumes();

        if(isMainMenuJustForVolSet) {
            enabled = false;
            return;
        }

        revSource = this.GetComponent<AudioSource>();

        boostSource = gameObject.AddComponent<AudioSource>();
        bumpSource = gameObject.AddComponent<AudioSource>();
        bumpSource.clip = bounceSound;
        goalSource = gameObject.AddComponent<AudioSource>();
        goalSource.clip = goalSound;

        revSource.clip = rev;
        revSource.loop = true;
        revSource.Play();
    }

    private void updateVolumes() {
        musicSource.volume = masterVol * musicVol;
    }

    public void setMasterVol(Slider changedSlider) {
        masterVol = changedSlider.value;
        updateVolumes();
    }
    public void setMusicVol(Slider changedSlider) {
        musicVol = changedSlider.value;
        updateVolumes();
    }
    public void setSoundVol(Slider changedSlider) {
        soundVol = changedSlider.value;
    }

    public void muteCarSound(bool muteCar) {
        isCarMuted = muteCar;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCarMuted)
        {
            revSource.volume = 0.0f;
            boostSource.loop = false;
        }
        else {
            revSource.volume = Mathf.Abs(Input.GetAxis("Vertical")) * 0.4f * masterVol * soundVol;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayBoostOneOff();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                PlayBoostTailLoop();
            }
        }

    }

    public void bangSound()
    {
        hitSource.volume = 0.5f * masterVol * soundVol;
        hitSource.Play();
    }
    public void bumpSound()
    {
        bumpSource.volume = 0.3f * masterVol * soundVol;
        bumpSource.Play();
    }

    public void winSong()
    {
        if (goalSource.isPlaying == false)
        {
            goalSource.volume = masterVol * musicVol;
            goalSource.Play();
        }
    }

    void PlayBoostOneOff()
	{
        boostSource.clip = boost;
        boostSource.loop = true;
        boostSource.volume = 0.4f * masterVol * soundVol;
        boostSource.Play();

	}
	void PlayBoostTailLoop()
	{
        boostSource.clip = boostTailLoop;
        boostSource.loop = false;
        boostSource.volume = 0.4f * masterVol * soundVol;
        boostSource.Play();

	}
}
