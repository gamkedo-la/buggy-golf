using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public HoleManager holeManager;

    [Header("Cameras")]
    public Camera carCam;
    public Camera ballCam;
    public Camera playResetCam;

    [Header("Play Reset Camera")]
    public float resetHeightFromBall;

    CameraFollow ballCamFollow;

    public void Start() {
        carCam = GameObject.FindGameObjectWithTag("CameraPlayer").GetComponent<Camera>();
        ballCam = GameObject.FindGameObjectWithTag("CameraBall").GetComponent<Camera>();
        ballCamFollow = ballCam.gameObject.GetComponent<CameraFollow>();
        playResetCam = GameObject.FindGameObjectWithTag("CameraPlayReset").GetComponent<Camera>();
        CameraHoleStart();
    }

    public void CameraHoleStart() { // Camera setup for start of hole
        ballCam.enabled = false;
        ballCamFollow.enabled = false;
        carCam.enabled = true;
        playResetCam.enabled = false;
    }

    public void CameraBallHitSwitch() { // Camera setup when ball is hit
        ballCam.transform.position = carCam.transform.position; // Put cameras in the same place
        ballCam.enabled = true;
        ballCamFollow.enabled = true;
        carCam.enabled = false;
    }

    public void CameraPlayResetSwitch() { // Camera setup to reset buggy for next stroke
        Vector3 ballPos = holeManager.ball.transform.position;
        playResetCam.transform.position = new Vector3(ballPos.x, (ballPos.y + resetHeightFromBall), ballPos.z);
        ballCam.enabled = false;
        ballCamFollow.enabled = false;
        playResetCam.enabled = true;
    }

}
