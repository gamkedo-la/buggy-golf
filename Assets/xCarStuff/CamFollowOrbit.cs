using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowOrbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offset;
    public Vector3 position;

    void Start()
    {
        offset = new Vector3(player.position.x + position.x, player.position.y + position.y, player.position.z + position.z) - player.position;
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}