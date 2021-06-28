using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public int CoinsForLevelFinish = 300;

    public Text CoinsAmountText;
    public Text CoinsForLevelFinishText;

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


    void Start()
    {
        Coins = 0; // TODO: save/load progress
        CoinsForLevelFinishText.text = CoinsForLevelFinish.ToString();
    }
}
