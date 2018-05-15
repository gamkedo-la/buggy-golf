using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakeToSoundManagerLevel : MonoBehaviour {
    public bool forMaster = false;
    public bool forMusic = false;
    public bool forSound = false;
	// Use this for initialization
	void Start () {
        Slider mySlider = GetComponent<Slider>();

        float valHere = 0.0f;
        if(forMaster) {
            valHere = SoundManager.masterVol;            
        }
        if (forMusic)
        {
            valHere = SoundManager.musicVol;
        }
        if (forSound)
        {
            valHere = SoundManager.soundVol;
        }

        mySlider.value = valHere;
	}
}
