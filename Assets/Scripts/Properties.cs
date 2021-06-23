using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{

    public enum Type
    {
        Player,
        Free,
        Enemy,
        Obstacle,
    };

    public Type ObjectType;
    public Renderer Renderer;
    public Animator Animator;

    private void OnValidate()
    {
        if (Renderer == null)
        {
            Renderer = GetComponent<Renderer>();
        }
        if (Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
        }
    }

    public static bool DoesTypeMatch(GameObject obj, Type type)
    {
        Properties props = obj.GetComponent<Properties>();
        return props!= null && props.ObjectType == type;
    }

    private void Start()
    {
        if (Animator == null || Animator.runtimeAnimatorController == null) return;
        Animator.SetFloat("offset", Random.Range(0.0f, 1.0f));
    }

}