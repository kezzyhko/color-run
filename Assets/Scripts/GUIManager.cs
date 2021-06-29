using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DELTation.UI.Screens;

public class GUIManager : MonoBehaviour
{

    public GameScreen Menu;
    public GameScreen PlayControls;
    public GameScreen WinScreen;
    public GameScreen LoseScreen;

    private GameScreen _currentScreen;

    public void ShowScreen(GameScreen screen)
    {
        if (_currentScreen) _currentScreen.Close();
        screen.Open();
        _currentScreen = screen;
    }

}