using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public GameObject PlayerPrefab;

    private ColorMixing _colorMixing;

    public void Construct(ColorMixing colorMixing)
    {
        _colorMixing = colorMixing;
    }

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject freeCrowd = collider.gameObject;
        Properties props = freeCrowd.GetComponent<Properties>();
        if (props == null || props.ObjectType != Properties.Type.FreeCrowd) return;

        // create new player
        Destroy(freeCrowd);
        GameObject newPlayer = Instantiate(PlayerPrefab, freeCrowd.transform.position, freeCrowd.transform.rotation);
        _colorMixing.Players.AddLast(newPlayer);

        // set color
        Color playerColor = GetComponent<Renderer>().material.color;
        newPlayer.GetComponent<Renderer>().material.color = playerColor;
    }

}