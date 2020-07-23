using System;
using UnityEngine;
using System.Collections.Generic;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public struct Move
    {
        //public readonly string playerName;
        public bool aTeamMove;
        public int moveIncrement;
        public bool haveMarked;

        public Move(bool isATeamMove, int increment, bool getTheRound)
        {
            //playerName = id.name;
            aTeamMove = isATeamMove;
            moveIncrement = increment;
            haveMarked = getTheRound;
        }
    }

    /// <summary>
    /// ARD
    /// </summary>
    public class RallyScorer : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Variable")]
        [SerializeField] private MatchData match;
        [Range(-3, 3)] public int rallyValue = 0;
        public List<Move> moveHistory = new List<Move>();

        private void Awake() => matchEvents = MatchEvents.Instance;

        public void MovedTo(int pos)
        {
            //calcul l'écart du move (nombre de point marqué)
            int increment = pos - rallyValue;

            //Save la nouvelle position jeton
            rallyValue = pos;

            //Si la pos à atteint une des extrémité, le point est marqué
            bool pointMarked = Mathf.Abs(pos) == 3 ? true : false;

            //Construction du move à save
            Move currentMove = new Move(true, increment, pointMarked);

            //Move Storage
            moveHistory.Add(currentMove);

            //Event du move
            matchEvents.Exchange();

            //Event du point si il y en a un
            if (pointMarked)
            {
                matchEvents.PointMarked();
                ResetRally();
            }
        }
        public void IncrementOf(int increment)
        {
            increment = match.teamA_Turn ? increment : -increment;

            //calcul la nouvelle position du jeton
            int pos = rallyValue + increment;

            //Clamp la nouvelle pos (pour ne pas sortir de la range
            pos = Mathf.Clamp(pos, -3, 3);

            //Save la nouvelle position jeton
            rallyValue = pos;

            //Si la pos à atteint une des extrémité, le point est marqué
            bool pointMarked = Mathf.Abs(pos) == 3 ? true : false;

            //Construction du move à save
            Move currentMove = new Move(true, increment, pointMarked);

            //Move Storage
            moveHistory.Add(currentMove);

            //Event du move
            matchEvents.Exchange();

            //Event du point si il y en a un
            if (pointMarked)
            {
                matchEvents.PointMarked();
                ResetRally();
            }
        }

        public void ResetRally()
        {
            rallyValue = 0;
        }

    }
}
