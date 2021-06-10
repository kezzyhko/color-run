using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperties : MonoBehaviour
{

    public enum Team
    {
        Player,
        FreeCrowd,
        Enemy,
    };

    public Team team;

}
