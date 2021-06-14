using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float Speed = 90.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * Speed, 0));
    }
}