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
    public scorecardUIScript scorecard;


    public void UIUpdateStroke(int newStroke) {
        strokeCount.text = newStroke.ToString();
    }

    public void UIUpdatePar(int newPar)
    {
        holePar.text = newPar.ToString();
    }

    public void UIUpdateScorecard(int par, int holescore, int gamescore) {
        scorecard.par.text = par.ToString();
        scorecard.gameScore.text = gamescore.ToString();
        scorecard.holeScore.text = holescore.ToString();
    }
        
    }
