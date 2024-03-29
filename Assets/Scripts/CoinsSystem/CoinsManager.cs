using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinsSystem
{
    public class CoinsManager : MonoBehaviour
    {

        public int CoinsForLevelFinish = 300;

        private const string CoinsAmountPlayerPrefsKey = "Coins";
        private int _coins;
        public event System.Action<int> CoinsAmountChanged;
        public int Coins
        {
            get
            {
                return _coins;
            }
            set
            {
                _coins = value;
                PlayerPrefs.SetInt(CoinsAmountPlayerPrefsKey, value);
                PlayerPrefs.Save();
                if (CoinsAmountChanged != null) CoinsAmountChanged(value);
            }
        }

        void Start()
        {
            Coins = PlayerPrefs.GetInt(CoinsAmountPlayerPrefsKey, 0);
        }
    }
}