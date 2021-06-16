using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMillColor : MonoBehaviour
{

    public ChangeColor.AcceptableColor ColorName;

    [SerializeField, HideInInspector]
    private GameObject[] _blades;

    void Start()
    {
        foreach (GameObject blade in _blades)
        {
            blade.AddComponent<ChangeColor>();
            blade.GetComponent<ChangeColor>().ColorName = ColorName;
        }
    }

}