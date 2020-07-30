using System;
using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class TurnManager : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private RallyScorer rallyScoreur;
        [SerializeField] private PartyScore partyScore;

        [Header("Variable")]
        [SerializeField] private MatchData match;

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void Start()
        {
            matchEvents.MatchStart();
        }

        private void OnEnable()
        {
            matchEvents.onExchange += OnExchange;
            matchEvents.onGameMarked += OnResetGame;
        }
        private void OnDisable()
        {
            matchEvents.onExchange -= OnExchange;
            matchEvents.onGameMarked -= OnResetGame;
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
            match.teamA_Turn = match.teamA_HaveService;
        }
    
    }
}
