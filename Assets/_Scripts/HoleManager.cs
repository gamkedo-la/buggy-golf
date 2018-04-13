using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour {

    [Header("Main")]
    public BallScript ball;
    public BuggyScript player;
    

    [Header("Managers")]
    public HoleManager holeManager;
    public ScoreManager scoreManager;
    public UIManager uiManager;
    public CameraManager cameraManager;
    public CarManager carManager;
    public BallManager ballManager;
    public ClubManager clubManager;

    [Header("Stroke")]
    public int currentStroke;
    public bool strokeOver = false;

    public void Start() {
        currentStroke = 1;
    }

    public void StrokeReset() {
        strokeOver = true; // Use this to keep certain things from happening in other scripts (car control, etc)
        ballManager.ReorientBall(ball); // Keep the ball in position but make it point straight up
        currentStroke++;
        cameraManager.CameraPlayResetSwitch(); // Change camera to top view to choose starting position
        ball.carResetter.gameObject.SetActive(true);
        ball.carResetterCage.gameObject.SetActive(true);
        ball.carResetter.ResetterStartReset();
    }
    
}
