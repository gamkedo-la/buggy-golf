using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public HoleManager holeManager;
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

    public void UIUpdatePar(string newPar)
    {
		holePar.text = newPar;
    }

	public void UIUpdateScorecard(int par, int holescore, int gamescore, string name) {
        scorecard.par.text = par.ToString();
        scorecard.gameScore.text = gamescore.ToString();
        scorecard.holeScore.text = holescore.ToString();
		scorecard.holeName.text = name;
    }

    public void UIUpdateScore(int score) {
        currentScore.text = score.ToString();
    }
		

    public void ButtonContinueNextHole() {
        holeManager.NextHole();
    }

	public void UIUpdateHoleName(string newHoleName) {
		holeName.text = newHoleName;
	}
        
    }
