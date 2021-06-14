using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float Speed = 2.0f;

    void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * Speed);
    }
}