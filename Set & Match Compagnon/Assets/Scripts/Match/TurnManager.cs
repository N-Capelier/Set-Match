using System;
using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class TurnManager : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private RallyScorer rallyScoreur;
        [SerializeField] private PartyScore partyScore;

        [Header("Variable")]
        [SerializeField] private MatchData match;

        private void OnEnable()
        {
            rallyScoreur.onExchange += OnExchange;

            partyScore.onTeamA_GameMarked += OnResetGame;
            partyScore.onTeamB_GameMarked += OnResetGame;

        }
        private void OnDisable()
        {
            rallyScoreur.onExchange -= OnExchange;

            partyScore.onTeamA_GameMarked += OnResetGame;
            partyScore.onTeamB_GameMarked += OnResetGame;

        }

        public void OnExchange()
        {
            match.teamA_Turn = !match.teamA_Turn;
        }

        private void OnResetGame()
        {
            //Changement de serveur
            match.teamA_HaveService = !match.teamA_HaveService;
            
            //Au tour du serveur
            if (match.teamA_HaveService)
            {
                match.teamA_Turn = match.teamA_HaveService;
            }
            else
            {
                match.teamA_Turn = match.teamA_HaveService;
            }
        }
    
    }
}
