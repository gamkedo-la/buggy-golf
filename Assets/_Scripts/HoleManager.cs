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
        uiManager.UIUpdateStroke(currentStroke);
    }

    public void HolePlayStart() {
        cameraManager.CameraHoleStart();
        ball.BallStartStroke();
    }

    public void StrokeReset() {
        strokeOver = true; // Use this to keep certain things from happening in other scripts (car control, etc)
        ballManager.ReorientBall(ball); // Keep the ball in position but make it point straight up
        currentStroke++;
        uiManager.UIUpdateStroke(currentStroke); // Update the stroke count UI
        cameraManager.CameraPlayResetSwitch(); // Change camera to top view to choose starting position
        uiManager.carResetter.ResetterStartReset();
    }
    
}
