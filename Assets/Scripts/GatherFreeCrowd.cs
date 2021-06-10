using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFreeCrowd : MonoBehaviour
{

    public GameObject currentColorIndicator;

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject newPlayer = collider.gameObject;
        CharacterProperties props = newPlayer.GetComponent<CharacterProperties>();
        if (props.team != CharacterProperties.Team.FreeCrowd) return;

        // change team
        props.team = CharacterProperties.Team.Player;

        // change color
        Color currentColor = currentColorIndicator.GetComponent<UnityEngine.UI.Image>().color;
        newPlayer.GetComponent<Renderer>().material.color = currentColor;

        // add necessary scripts
        newPlayer.AddComponent<GatherFreeCrowd>();
        newPlayer.AddComponent<PlayerControl>();
    }

}
