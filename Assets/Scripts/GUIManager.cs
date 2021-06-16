using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    public GameObject PlayControls;

    public GameObject EndLevelScreen;
    public UnityEngine.UI.Text EndLevelStatusText;

    public void EndLevel(bool isWin)
    {
        EndLevelScreen.SetActive(true);
        if (isWin)
        {
            EndLevelStatusText.text = "Level Complete!";
        }
        else
        {
            EndLevelStatusText.text = "Try again";
        }
    }
}
