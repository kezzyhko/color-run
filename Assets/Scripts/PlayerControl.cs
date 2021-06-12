using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float ForwardMoveSpeed = 2.0f;
    public float HorizontalMoveSpeed = 2.0f;

    void Update()
    {
        transform.position += new Vector3(
            Time.deltaTime * HorizontalMoveSpeed * Input.GetAxis("Horizontal"),
            0,
            Time.deltaTime * ForwardMoveSpeed
        );
    }
}