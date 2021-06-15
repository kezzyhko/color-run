using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    private ColorMixing _colorMixing;

    public void Construct(ColorMixing colorMixing)
    {
        _colorMixing = colorMixing;
    }

    void OnTriggerEnter(Collider collider)
    {
        // get info
        GameObject obstacle = collider.gameObject;
        Properties props = obstacle.GetComponent<Properties>();
        if (props == null || props.ObjectType != Properties.Type.Obstacle) return;

        Color playerColor = GetComponent<Renderer>().sharedMaterial.color;
        Color obstacleColor = obstacle.GetComponent<Renderer>().sharedMaterial.color;
        if (ColorMixing.CompareWithoutAlpha(playerColor, obstacleColor))
        {
            // player matched the colors, destroy obstacle
            Destroy(obstacle);
        }
        else
        {
            // player didn't match the colors, destroy player
            Destroy(gameObject);
        }
    }

}