using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinsSystem
{
    public class CoinsManager : MonoBehaviour
    {

        public int CoinsForLevelFinish = 300;

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
                if (CoinsAmountChanged != null) CoinsAmountChanged(value);
            }
        }

        void Start()
        {
            Coins = 0; // TODO: save/load progress
        }
    }
}
