using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarResetter : MonoBehaviour {

    public bool resettingCar = false;

    public GameObject carPlacer;

    public float placementSpeed = 1;

    [Header("Ranges")]
    public float rangeXMax = 0f;
    public float rangeXMin = 0f;
    public float rangeYMax = 0f;
    public float rangeYMin = 0f;
    


    void FixedUpdate()
    {
        if (resettingCar)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                carPlacer.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * placementSpeed);
            }
            if (Input.GetAxis("Tilt") != 0)
            {
                carPlacer.transform.Translate(Vector3.up * Input.GetAxis("Tilt") * placementSpeed);
            }
            Vector2 placerPosition = carPlacer.transform.localPosition;
            float placerX = (Mathf.Clamp(placerPosition.x, rangeXMin, rangeXMax));
            float placerY = (Mathf.Clamp(placerPosition.y, rangeYMin, rangeYMax));
            carPlacer.transform.localPosition = new Vector2(placerX, placerY);

            //this.transform.LookAt(ball.transform);

        }
    }
            public void ResetterStartReset()
    {
        resettingCar = true;
    }

}













    // FIRST ATTEMPT, DIDN"T WORK
    /*public bool resettingCar = false;
    public GameObject ball;
    MeshRenderer meshRenderer;


    public void ResetterStartReset() {
        resettingCar = true;
        this.transform.position = ball.transform.position;
       // this.meshRenderer.enabled = true;
    }
    
    // Use this for initialization
	void Start () {
        //resettingCar = false;
        //this.meshRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (resettingCar) {
            if (Input.GetAxis("Horizontal") != 0)
            {
                transform.Translate(Vector3.right * Input.GetAxis("Horizontal"));
            }
            if (Input.GetAxis("Tilt") != 0)
            {
                transform.Translate(Vector3.forward * Input.GetAxis("Tilt"));
            }
            //this.transform.LookAt(ball.transform);
        }

	}*/

