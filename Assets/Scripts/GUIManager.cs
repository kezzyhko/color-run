using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DELTation.UI.Screens;

public class GUIManager : MonoBehaviour
{

    public GameObject Menu;
    public GameObject PlayControls;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private GameObject _currentScreen;

    public void ShowScreen(GameObject screen)
    {
        if (_currentScreen) _currentScreen.GetComponent<GameScreen>().Close();
        screen.GetComponent<GameScreen>().Open();
        _currentScreen = screen;
    }

}