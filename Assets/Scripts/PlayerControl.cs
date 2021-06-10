using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float forwardMoveSpeed = 2.0f;
    public float horizontalMoveSpeed = 2.0f;

    void Update()
    {
        transform.position += new Vector3(
            Time.deltaTime * horizontalMoveSpeed * Input.GetAxis("Horizontal"),
            0,
            Time.deltaTime * forwardMoveSpeed
        );
    }
}