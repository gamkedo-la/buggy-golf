using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downforce : MonoBehaviour {
    
    Rigidbody rb;
    public float liftCoefficient; // Use negative for downforce
    public bool worldForce = false;

    void Start()
    {
        Transform trs = transform;
        rb = trs.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            float lift = liftCoefficient * rb.velocity.sqrMagnitude;
            if (worldForce)
            {
                rb.AddForceAtPosition(lift * Vector3.up, transform.position);    
            }
            else{
                rb.AddForceAtPosition(lift * transform.up, transform.position);    
            }


        }
    }
}
