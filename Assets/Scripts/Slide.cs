using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{

    public float Speed = 2.0f;

    private float _platformWidth;

    public void Construct(BoxCollider platformCollider)
    {
        _platformWidth = platformCollider.transform.localScale.x;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) - _platformWidth / 2 > 0)
        {
            Speed = -Speed;
        }
        transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
    }
}