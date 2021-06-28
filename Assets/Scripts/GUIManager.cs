using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    public GameObject PlayControls;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private GameObject _currentScreen;

    private void Start()
    {
        _currentScreen = PlayControls;
    }

    public void ShowScreen(GameObject screen)
    {
        if (_currentScreen) _currentScreen.SetActive(false);
        screen.SetActive(true);
        _currentScreen = screen;
    }

}
