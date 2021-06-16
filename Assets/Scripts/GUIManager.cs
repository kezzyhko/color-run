using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    public GameObject PlayControls;
    public GameObject EndLevelScreen;

    public void EndLevel(bool isWin)
    {
        EndLevelScreen.SetActive(true);
        if (isWin)
        {
            
        }
    }
}
