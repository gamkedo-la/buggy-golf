using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

   public void ReorientBall(BallScript ball) { // Make the ball point straight up and reset for play
        ball.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //ball.ballActive = false;
    }
    
}
