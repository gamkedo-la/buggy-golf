using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoleManager : MonoBehaviour {

    [Header("Main")]
    public BallScript ball;
    public CarAcrobatics player;
    public holeScript hole;
    public int holePar = 1;
	public int playerPar = 0;
    public string holeName;
    public BallInPlayScript ballInPlay;

    public bool holeStarted;

    [Header("Managers")]
    public CarManager carManager;
    public ClubManager clubManager;

    [Header("Stroke Tracking")]
    public int currentStroke;
    public bool strokeOver = false; // Play has stopped
    public bool holeOver = false; // Placholder bool, may use by something else
	public OutOfPlayScript outOfPlayScript;
	public Vector3 lastBallPosition;

	[Header("Debug")]
	public GameObject playerManagerPrefab;

	[Header("Misc")]
	public Text boostTip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static HoleManager Instance { get; private set; }

    public void Start() {

        currentStroke = 1;

        //Link the player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CarAcrobatics>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallScript>();

		//Link the managers
        carManager = GameObject.FindGameObjectWithTag("CarManager").GetComponent<CarManager>();
        clubManager = GameObject.FindGameObjectWithTag("ClubManager").GetComponent<ClubManager>();
        boostTip = GameObject.FindGameObjectWithTag("BoostTip").GetComponent<Text>();
        outOfPlayScript = GameObject.FindGameObjectWithTag("OutOfPlay").GetComponent<OutOfPlayScript>();

		// UI UPDATES
        UIManager.Instance.UIUpdateHoleName(holeName); //Update the Hole's name in the UI
        SetPlayerPar (PlayerManager.Instance.playerHandicap); //Calculate player's par based on handicap
        UIManager.Instance.UIUpdateStroke(currentStroke); // Update Stroke count in the UI
        UIManager.Instance.UIUpdatePar(playerPar.ToString() /*+ "(" + holePar.ToString() + ")"*/);

		//Set par for the hole adjusted for handicap
        SetHolePar(playerPar);

        //Update Running Score
        UIManager.Instance.UIUpdateScore(PlayerManager.Instance.localScoreHolder);

		//Get Ball position for possible mulligan/out of play
        SetBallLastPosition();

        // Start Play
        HolePlayStart();

    }

    public void HolePlayStart()
    {

        player.ballInPlayScript.ResetHit();
        CameraManager.Instance.CameraHoleStart();
        ball.BallStartStroke();
        holeStarted = true;
        UIManager.Instance.scorecard.canvas.enabled = false;
    }

    // PAR

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

    // ACTIONS WHEN WE COMPLETE A HOLE
	public void HoleEnd() {
        holeOver = true;
        ScoreManager.Instance.AddLocalScore(currentStroke - holePar); // Calculate and add score to the leaderboard local variable
        UIManager.Instance.UIUpdateStroke(currentStroke); // Update UI
        UIManager.Instance.UIUpdatePar(holePar.ToString());
        int local = ScoreManager.Instance.GetLocalScore(); // Retrieve calculation of local score
        PlayerManager.Instance.HoldScore(local);
        UIManager.Instance.UIUpdateScorecard(holePar, currentStroke, local, holeName); // Update Scorecard UI with all of this
        UIManager.Instance.scorecard.canvas.enabled = true;
		boostTip.enabled = false;
    }

	// ACTIONS WHEN WE END A SHOT
	public void StrokeReset(bool mulligan) {
        strokeOver = true; // Use this to keep certain things from happening in other scripts (car control, etc)
		if (mulligan) { // Is this a successful hit or are we resetting and losing a stroke?
            BallManager.Instance.ResetBallToLast(ball, lastBallPosition);
		} else {
            BallManager.Instance.ReorientBall(ball); // Keep the ball in position but make it point straight up
		}
        currentStroke++;
        UIManager.Instance.UIUpdateStroke(currentStroke); // Update the stroke count UI
        CameraManager.Instance.CameraPlayResetSwitch(); // Change camera to top view to choose starting position
        UIManager.Instance.carResetter.ResetterStartReset();
		SetBallLastPosition ();
    }

	public void SetBallLastPosition(){
		lastBallPosition = ball.gameObject.transform.position;
	}

    public void NextHole() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        holeStarted = false;
        SceneManager.LoadScene(0);
    }

}
