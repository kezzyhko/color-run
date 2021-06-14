using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{

    public float Speed = 2.0f;
    public GameObject Platform;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) - Platform.transform.localScale.x / 2 > 0)
        {
            Speed = -Speed;
        }
        transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
    }
}