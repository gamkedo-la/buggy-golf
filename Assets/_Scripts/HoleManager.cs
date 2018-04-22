using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour {

    [Header("Main")]
    public BallScript ball;
    public BuggyScript player;
    public holeScript hole;
    public int holePar = 1;
    public string holeName;
    

    [Header("Managers")]
    public HoleManager holeManager;
    public ScoreManager scoreManager;
    public UIManager uiManager;
    public CameraManager cameraManager;
    public CarManager carManager;
    public BallManager ballManager;
    public ClubManager clubManager;

    [Header("Stroke Tracking")]
    public int currentStroke;
    public bool strokeOver = false; // Play has stopped
    public bool holeOver = false; // Placholder bool, may use by something else

    public void Start() {
        currentStroke = 1;
        uiManager.UIUpdateStroke(currentStroke);
        uiManager.UIUpdatePar(holePar);
    }

    public void SetHolePar(int newPar) {
        holePar = newPar;
    }

    public void HolePlayStart() {
        cameraManager.CameraHoleStart();
        ball.BallStartStroke();
        uiManager.scorecard.canvas.enabled = false;
    }

    public void HoleEnd() {
        holeOver = true;
        scoreManager.AddLocalScore(currentStroke - holePar); // Calculate and add score to the leaderboard local variable
        uiManager.UIUpdateStroke(currentStroke); // Update UI
        uiManager.UIUpdatePar(holePar);
        int local = scoreManager.GetLocalScore(); // Retrieve calculation of local score
        uiManager.UIUpdateScorecard(holePar, currentStroke, local); // Update Scorecard UI with all of this
        uiManager.scorecard.canvas.enabled = true;
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
