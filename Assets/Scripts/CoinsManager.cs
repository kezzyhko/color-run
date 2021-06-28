using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public Text CoinsAmountText;

    private int _coins;
    
    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;
            CoinsAmountText.text = Coins.ToString();
        }
    }

    public const int CoinsForLevelFinish = 300;

    void Start()
    {
        Coins = 0; // TODO: save/load progress
    }
}
