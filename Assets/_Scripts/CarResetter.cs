using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarResetter : MonoBehaviour {

    public bool resettingCar = false;
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

	}
}
