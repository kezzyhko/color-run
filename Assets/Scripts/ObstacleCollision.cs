using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    public ColorMixing colorMixingScript;

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject obstacle = collider.gameObject;
        Properties props = obstacle.GetComponent<Properties>();
        if (props == null || props.type != Properties.Type.Obstacle) return;

        Color playerColor = GetComponent<Renderer>().material.color;
        Color obstacleColor = obstacle.GetComponent<Renderer>().material.color;
        if (ColorMixing.CompareWithoutAlpha(playerColor, obstacleColor))
        {
            // player matched the colors, destroy obstacle
            Destroy(obstacle);
        }
        else
        {
            // player didn't match the colors, destroy player
            colorMixingScript.players.Remove(gameObject);
            Destroy(gameObject);
        }
    }

}