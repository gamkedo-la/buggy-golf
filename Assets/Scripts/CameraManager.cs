using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [Header("Cameras")]
    public Camera playResetCam;
    public Transform playerTarget;
    public Transform ballTarget;

    bool setCamPlayer = false;
    bool setCamBall = false;

    [Header("Play Reset Camera")]
    public float resetHeightFromBall;

    bool initialized = false;

    //CameraFollow ballCamFollow;

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

    public static CameraManager Instance { get; private set; }

    public void CameraHoleStart() { // Camera setup for start of hole
        playResetCam = GameObject.FindGameObjectWithTag("CameraPlayReset").GetComponent<Camera>();
        playerTarget = 
        ballTarget = HoleManager.Instance.ball.transform;
        Debug.Log("DEBUG " + this.name + ": CameraHoleStart() ran");
        SetCameraToPlayer();
    }

    public void CameraBallHitSwitch() { // Camera setup when ball is hit
        SetCameraToBall();
        Debug.Log("DEBUG " + this.name + ": CameraBallHitSwitch() ran");
    }

    public void CameraPlayResetSwitch() { // Camera setup to reset buggy for next stroke
        Vector3 ballPos = HoleManager.Instance.ball.transform.position;
        playResetCam.transform.position = new Vector3(ballPos.x, (ballPos.y + resetHeightFromBall), ballPos.z);
        CameraBuggyFollowOrbit.Instance.camera.enabled = false;
        playResetCam.enabled = true;
        Debug.Log("DEBUG " + this.name + ": CameraPlayResetSwitch() ran");
    }

    public void SetCameraToPlayer ()
    {
        Debug.Log("DEBUG " + this.name + ": setting new target to: " + HoleManager.Instance.player.transform);
        CameraBuggyFollowOrbit.Instance.SetTarget(HoleManager.Instance.player.gameObject, true);

  
    }

    public void SetCameraToBall()
    {
        Debug.Log("DEBUG " + this.name + ": setting new target to: " + HoleManager.Instance.ball.transform);
        CameraBuggyFollowOrbit.Instance.SetTarget(HoleManager.Instance.ball.gameObject, false);
    }
}
