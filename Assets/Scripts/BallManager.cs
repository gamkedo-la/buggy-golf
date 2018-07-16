using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

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
    public static BallManager Instance { get; private set; }

   public void ReorientBall(BallScript ball) { // Make the ball point straight up and reset for play
        ball.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //ball.ballActive = false;
   }
	public void ResetBallToLast(BallScript ball, Vector3 lastPosition){
		ball.gameObject.transform.position = lastPosition;
		ReorientBall (ball);
	}
}
