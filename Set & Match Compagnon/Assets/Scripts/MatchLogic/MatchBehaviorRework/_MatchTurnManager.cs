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
        [SerializeField] private MatchEvents matchEvents;
        [SerializeField] private MatchData match;

        [Header("Variable")]
        public List<string> matchPlayers = new List<string>();
        public int turnOfPlayer = 0;

        private void Awake() => matchEvents = MatchEvents.Instance;
        private void OnEnable() => matchEvents.onMatchStart += Initialisation;
        private void OnDisable() => matchEvents.onMatchStart -= Initialisation;
        
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
            this.turnOfPlayer = turnOfPlayer;
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
