using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{

    public float Speed = 2.0f;
    public float Amplitude = 5.0f;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) - Amplitude / 2 > 0)
        {
            Speed = -Speed;
        }
        transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
    }
}