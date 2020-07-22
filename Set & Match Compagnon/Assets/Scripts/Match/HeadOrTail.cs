using System;
using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class HeadOrTail : Singleton<HeadOrTail>
    {
        [Header("Coin Button"), SerializeField]
        private Button coinButton;
        [Header("StartGame Button"), SerializeField]
        private Button startMatchButton;

        [Header("Toggles")]
        [SerializeField] private Toggle firstPlayerHead;
        [SerializeField] private Toggle firstPlayerTail, secondPlayerHead, secondPlayerTail;

        [Header("Variable")]
        public bool havelauch = false;
        public bool result_Head = true;
        [Space(10)]
        public bool teamA_Head = true;
        public bool teamA_Win = true;

        #region CoinLauchEvent
        //Event OnCoinLauch
        public event Action onCoinLauch;
        public void CoinLauch()
        {
            onCoinLauch?.Invoke();
        }
        #endregion

        private void Update()
        {
            //Si la piece n'a pas été lancée
            if (!havelauch) 
            {
                //Team A Head and Team B Tail
                if (firstPlayerHead.isOn & secondPlayerTail.isOn)
                {
                    teamA_Head = true;

                    coinButton.interactable = true;
                }
                else
                //Team A Tail and Team B Head
                if (firstPlayerHead.isOn & secondPlayerTail.isOn || firstPlayerTail.isOn & secondPlayerHead.isOn)
                {
                    teamA_Head = false;

                    coinButton.interactable = true;
                }
                //Haven't make choice
                else
                {
                    coinButton.interactable = false;
                }
            }
            
            //Peut on appuyer
            if (havelauch)
            {
                startMatchButton.interactable = true;
            }
            else
            {
                startMatchButton.interactable = false;
            }
        }

        public void HeadOrTailLaunch()
        {
            // 0-1-2-3-4 means Head win & 5-6-7-8-9 means Tail win
            if (UnityEngine.Random.Range(0, 10) < 5)//Head Win 
            {
                result_Head = true;

                if (teamA_Head)
                {
                    teamA_Win = true;
                }
                else
                {
                    teamA_Win = false;
                }
            }
            else //Tail Win
            {
                result_Head = false;

                if (teamA_Head)
                {
                    teamA_Win = false;
                }
                else
                {
                    teamA_Win = true;
                }
            }
            
            CoinLauch();
            
            havelauch = true;
        }

    }
}
