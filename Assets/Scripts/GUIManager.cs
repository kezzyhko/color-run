using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    public GameObject Menu;
    public GameObject PlayControls;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private GameObject _currentScreen;

    public void ShowScreen(GameObject screen)
    {
        if (_currentScreen) _currentScreen.SetActive(false);
        screen.SetActive(true);
        _currentScreen = screen;
    }

}
