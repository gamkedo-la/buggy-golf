using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBuggyFollowOrbit : MonoBehaviour
{
    [HideInInspector]
    public Camera camera;
    public float turnSpeed = .75f;
    public GameObject target;


    private Vector3 offset;
    // public Vector3 position;

    public float buggyDistBack = 5f;
    public float buggyDistAbove = 2f;

    public float ballDistBack = 10f;
    public float ballDistAbove = 2.5f;

    public float buggyAfterLookAngleAdjust = -15f;
    public float buggyAfterLookHeightAdjust = .25f;

    public float ballAfterLookAngleAdjust;
    public float ballAfterLookHeightAdjust;

    bool followPlayer = true;

    Vector3 newOffset;

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

    public static CameraBuggyFollowOrbit Instance { get; private set; }


    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (HoleManager.Instance.holeStarted && target != null)
        {
            if (followPlayer)
            {
                Follow(buggyDistBack,
                       buggyDistAbove,
                       buggyAfterLookHeightAdjust,
                       buggyAfterLookAngleAdjust);
                Debug.Log("DEBUG " + this.name + ": following Buggy.");
            }
            else
            {
                Follow(ballDistBack,
                       ballDistAbove,
                       ballAfterLookHeightAdjust,
                       ballAfterLookAngleAdjust);
                Debug.Log("DEBUG " + this.name + ": following Ball.");
            }
        }
    }
   

    public void SetTarget(GameObject newTarget, bool targetIsPlayer)
    {
        target = newTarget;
        Debug.Log("DEBUG " + this.name + ": new target is :" + newTarget);
        followPlayer = targetIsPlayer;
        RefreshOffset();
        EnableCamera();
        Debug.Log("DEBUG " + this.name + ": SetTarget() ran");
    }

    public void RefreshOffset()
    {
        if(followPlayer)
        {
            offset = (target.transform.position - (target.transform.forward * buggyDistBack)) - target.transform.position;
        }
        else
        {
            offset = (target.transform.position - (target.transform.forward * ballDistBack)) - target.transform.position;
        }

        Debug.Log("DEBUG " + this.name + ": RefreshOffset() ran");
    }

    void Follow(float distBack, float distAbove, float afterLookHeightAdjust, float afterLookAngleAdjust)
    {

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        Vector3 altitude = new Vector3(0, distAbove, 0);
        transform.position = target.transform.position + offset + altitude;
        transform.LookAt(target.transform.position);

        // Adjust height and angle after the relative positioning
        Vector3 heightAdjust = new Vector3(0, afterLookHeightAdjust, 0);
        Vector3 angleAdjust = new Vector3(afterLookAngleAdjust, 0, 0);
        transform.position = transform.position + heightAdjust;
        transform.Rotate(angleAdjust);
    }

    void EnableCamera()
    {
        camera.enabled = true;
    }
}