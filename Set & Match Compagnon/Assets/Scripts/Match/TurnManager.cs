using System;
using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    [Serializable]
    public struct Team
    {
        public string name;
        public bool duo;
    }

    /// <summary>
    /// ARD
    /// </summary>
    public class TurnManager : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private RallyScorer rallyScoreur;
        [SerializeField] private PartyScore partyScore;

        [Header("Variable")]
        public Team TeamA;
        public Team TeamB;

        public bool teamA_turn;
        public bool teamA_serving;

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

        private void Update()
        {
            teamA_serving = partyScore.TeamA_haveService;
        }

        public void OnExchange()
        {
            teamA_turn = !teamA_turn;
        }
        private void OnResetGame()
        {
            if (partyScore.TeamA_haveService)
            {
                teamA_serving = true;
                teamA_serving = teamA_turn;
            }
            else
            {
                teamA_serving = false;
                teamA_serving = teamA_turn;
            }
        }
    
    }
}
