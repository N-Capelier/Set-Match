using System;
using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class HeadOrTail : MonoBehaviour
    {
        [Header("GameEvent")]
        [SerializeField] private MatchEvents matchEvents;

        [Header("Coin Button"), SerializeField]
        private Button coinButton;
        [Header("StartGame Button"), SerializeField]
        private Button startMatchButton;

        [Header("Toggles")]
        [SerializeField] private Toggle firstPlayerHead;
        [SerializeField] private Toggle firstPlayerTail;
        [SerializeField] private Toggle secondPlayerHead;
        [SerializeField] private Toggle secondPlayerTail;

        [Header("Variable")]
        [SerializeField] private MatchData match;
        [Space(10)]
        public bool haveBeenlauch = false;
        public bool coinResult_Head = true;
        public bool teamA_ChooseHead = true;

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void Update()
        {
            //Si la piece n'a pas été lancée
            if (!haveBeenlauch) 
            {
                //Team A Head and Team B Tail
                if (firstPlayerHead.isOn & secondPlayerTail.isOn)
                {
                    teamA_ChooseHead = true;

                    coinButton.interactable = true;
                }
                else
                //Team A Tail and Team B Head
                if (firstPlayerHead.isOn & secondPlayerTail.isOn || firstPlayerTail.isOn & secondPlayerHead.isOn)
                {
                    teamA_ChooseHead = false;

                    coinButton.interactable = true;
                }
                //Haven't make choice
                else
                {
                    coinButton.interactable = false;
                }
            }
            
            //Peut on appuyer
            if (haveBeenlauch)
            {
                startMatchButton.interactable = true;
            }
            else
            {
                startMatchButton.interactable = false;
            }
        }

        public void OnHeadOrTailLaunch()
        {
            // 0-1-2-3-4 means Head win & 5-6-7-8-9 means Tail win
            if (UnityEngine.Random.Range(0, 10) < 5)//Head Win 
            {
                coinResult_Head = true;

                if (teamA_ChooseHead)
                {
                    match.teamA_StartServing = true;
                    match.teamA_HaveService = true;
                    match.teamA_Turn = true;
                }
                else
                {
                    match.teamA_StartServing = false;
                    match.teamA_HaveService = false;
                    match.teamA_Turn = false;
                }
            }
            else //Tail Win
            {
                coinResult_Head = false;

                if (teamA_ChooseHead)
                {
                    match.teamA_StartServing = false;
                    match.teamA_HaveService = false;
                    match.teamA_Turn = false;
                }
                else
                {
                    match.teamA_StartServing = true;
                    match.teamA_HaveService = true;
                    match.teamA_Turn = true;
                }
            }

            haveBeenlauch = true;
        }

    }
}
