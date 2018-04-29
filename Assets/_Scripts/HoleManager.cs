using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleManager : MonoBehaviour {

    [Header("Main")]
    public BallScript ball;
    public BuggyScript player;
    public holeScript hole;
    public int holePar = 1;
	public int playerPar = 0;
    public string holeName;
    

    [Header("Managers")]
    public HoleManager holeManager;
    public ScoreManager scoreManager;
    public UIManager uiManager;
    public CameraManager cameraManager;
    public CarManager carManager;
    public BallManager ballManager;
    public ClubManager clubManager;
	public PlayerManager playerManager;

    [Header("Stroke Tracking")]
    public int currentStroke;
    public bool strokeOver = false; // Play has stopped
    public bool holeOver = false; // Placholder bool, may use by something else

	[Header("Debug")]
	public GameObject playerManagerPrefab;

    public void Start() {
        currentStroke = 1;

		playerManager = null;

		//Link the managers
		holeManager = GameObject.FindGameObjectWithTag("HoleManager").GetComponent<HoleManager>();
		scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
		uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
		cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
		carManager = GameObject.FindGameObjectWithTag("CarManager").GetComponent<CarManager>();
		ballManager = GameObject.FindGameObjectWithTag("BallManager").GetComponent<BallManager>();
		clubManager = GameObject.FindGameObjectWithTag("ClubManager").GetComponent<ClubManager>();

		//FOR TESTING if a playermanager doesn't exist (comes from main menu), create one
		GameObject pm = GameObject.FindGameObjectWithTag("PlayerManager");
		if (pm != null) {

			playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();

		} 
		else {
			Debug.Log ("HoleManager: PlayerManager instantiated");
			playerManager = Instantiate (playerManagerPrefab).GetComponent<PlayerManager> ();
		}

		//playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();


		//Calculate player's par based on handicap
		SetPlayerPar (playerManager.playerHandicap);

		//Update UI before we modify the hole par
		uiManager.UIUpdateStroke(currentStroke);
		uiManager.UIUpdatePar(playerPar.ToString() + "(" + holePar.ToString() + ")");

		//Set par for the hole adjusted for handicap
		SetHolePar(playerPar);

    }

    public void SetHolePar(int newPar) {
        holePar = newPar;
    }

	void SetPlayerPar(int handicapLevel) {
		switch (handicapLevel) {
		case 3: // Hard
			playerPar = holePar-1;
			break;
		case 2: // Medium
			playerPar = holePar;
			break;
		case 1: // Easy
			playerPar = holePar+1;
			break;
		default: // Nothing
			playerPar = holePar;
			break;



		}
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
		uiManager.UIUpdatePar(holePar.ToString());
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

    public void NextHole() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
