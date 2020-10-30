using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    /// <summary> ARD script <para>
    /// Gère qui doit jouer à quel moment
    /// </para></summary>
    public class _MatchTurnManager : Singleton<_MatchTurnManager>
    {
        [Header("GameEvent")]
        [SerializeField] private MatchData match;

        [Header("Variable")]
        public List<string> matchPlayers = new List<string>();
        public static int turnOfPlayer = 0;
        public static bool isService = true;
        public static bool is2ndService = false;

        public int turnOfPlayerDebug = 0;
        public bool isServiceDebug = true;
        public bool is2ndServiceDebug = false;

        private void Update()
        {
            turnOfPlayerDebug = turnOfPlayer;
            isServiceDebug = isService;
            is2ndServiceDebug = is2ndService;
        }

        private void OnEnable() => MatchEvents.onMatchStart += Initialisation;
        private void OnDisable() => MatchEvents.onMatchStart -= Initialisation;
        
        private void Initialisation()
        {
            matchPlayers = new List<string>();

            if (match.doubleMatch)
            {
                matchPlayers.Add(match.teamA_Player1);
                matchPlayers.Add(match.teamB_Player1);
                matchPlayers.Add(match.teamA_Player2);
                matchPlayers.Add(match.teamB_Player2);
            }
            else
            {
                matchPlayers.Add(match.teamA_Player1);
                matchPlayers.Add(match.teamB_Player1);
            }
        }

        /// <summary> 
        /// Est ce que c'est le tout de l'équipe A ?
        /// </summary>
        public bool IsTeamATurn()
        {
            if(turnOfPlayer% 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary> 
        /// C'est au tour de l'équipe qui sert 
        /// </summary>
        public void ServiceTurn()
        {
            if (match.teamA_HaveService)
            {
                turnOfPlayer = 0;
            }
            else
            {
                turnOfPlayer = 1;
            }
        }

        public void TurnOf(int turnOfPlayer)
        {
            _MatchTurnManager.turnOfPlayer = turnOfPlayer;
        }
        public void NextTurn()
        {
            turnOfPlayer++;
            turnOfPlayer %= matchPlayers.Count;
        }
        public void PreviousTurn()
        {
            turnOfPlayer--;
            turnOfPlayer = Mathf.Abs(turnOfPlayer + matchPlayers.Count)%matchPlayers.Count;
        }
    }
}
