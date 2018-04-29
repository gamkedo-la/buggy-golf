using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCameraSwitcher : MonoBehaviour {

    public Camera carCam;
    public Camera ballCam;
    public Collider[] carColliders;
    public string ballTag;


    // Use this for initialization
    void Start() {

    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ballTag)
        {
            ballCam.transform.position = carCam.transform.position;
            ballCam.gameObject.SetActive(true);
            carCam.gameObject.SetActive(false);
        }
    }

}
