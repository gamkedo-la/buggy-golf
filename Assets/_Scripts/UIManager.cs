using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject speedometer;
    public GameObject clubIcon;
    public Text strokeCount;    
    public Text holeName;
    public Text holePar;
    public Text holeDistance;
    public Text currentScore;
    public CarResetter carResetter;


    public void UIUpdateStroke(int newStroke) {
        strokeCount.text = newStroke.ToString();
    }
}
