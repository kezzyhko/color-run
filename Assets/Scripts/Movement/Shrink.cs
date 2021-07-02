using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{

    public float ShrinkSpeed = 10.0f;

    void Update()
    {
        if (transform.localScale == Vector3.zero)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * ShrinkSpeed);
        }
    }
}