using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowOrbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform target;

    private Vector3 offset;
    public Vector3 position;

    void Start()
    {
        offset = new Vector3(target.position.x + position.x, target.position.y + position.y, target.position.z + position.z) - target.position;
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, Vector3.up) * offset;
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }

}