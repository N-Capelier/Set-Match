using Player;
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
        public bool headWin = false;

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
            if(firstPlayerHead.isOn & secondPlayerTail.isOn || firstPlayerTail.isOn & secondPlayerHead.isOn)
            {
                coinButton.interactable = true;
            }
            else
            {
                coinButton.interactable = false;
            }

            if (havelauch && !startMatchButton.interactable)
            {
                startMatchButton.interactable = true;
            }
            else 
            if(!havelauch && startMatchButton.interactable)
            {
                startMatchButton.interactable = false;
            }

        }

        public void SimplyfyHeadOrTailLaunch()
        {
            CoinLauch();

            // 0-1-2-3-4 means Head win & 5-6-7-8-9 means Tail win
            if (UnityEngine.Random.Range(0, 10) < 5)//Head Win 
            {
                headWin = true;
            }
            else //Tail Win
            {
                headWin = false;
            }
            havelauch = true;
        }

        public void HeadOrTailLaunch(PlayerScore HeadTeam, PlayerScore TailTeam)
        {
            CoinLauch();

            // 0-1-2-3-4 means Head win & 5-6-7-8-9 means Tail win
            if (UnityEngine.Random.Range(0, 10) < 5)//Head Win 
            {
                PlayerScoreUpdate(HeadTeam, TailTeam);
                headWin = true;
            }
            else //Tail Win
            {
                PlayerScoreUpdate(TailTeam, HeadTeam);
                headWin = false;
            }


            havelauch = true;
        }

        private void PlayerScoreUpdate(PlayerScore Winer, PlayerScore Loser)
        {
            //Team starting the Game saved in PlayerScore
            Winer.startsTheParty = true;
            Loser.startsTheParty = false;

            //Team serving saved in PlayerScore
            Winer.isServing = true;
            Loser.isServing = false;
        }

    }
}
