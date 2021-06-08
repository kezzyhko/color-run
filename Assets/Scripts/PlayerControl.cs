using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float forwardMoveSpeed = 2.0f;
    public float horizontalMoveSpeed = 2.0f;

    void Update()
    {
        float horizontalMoveAmount = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            horizontalMoveAmount -= 1;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            horizontalMoveAmount += 1;

        transform.position += new Vector3(
            Time.deltaTime * horizontalMoveSpeed * horizontalMoveAmount,
            0,
            Time.deltaTime * forwardMoveSpeed
        );
    }
}
