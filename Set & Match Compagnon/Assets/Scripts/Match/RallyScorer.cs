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
    public class RallyScorer : Singleton<RallyScorer>
    {
        [Header("Variable")]
        [Range(-3, 3)] public int rallyValue = 0;
        public List<Move> moveHistory = new List<Move>();

        #region Events
        //Event OnPointMarked
        public event Action onExchange;
        public event Action onPointMarked;
        public void Exchange() => onExchange?.Invoke();
        public void PointMarked() => onPointMarked?.Invoke();
        #endregion

        private void Update()
        {
            //The Rally always keep in the game range
            rallyValue = Mathf.Clamp(rallyValue, -3, 3);
        }

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
            Exchange();

            //Event du point si il y en a un
            if (pointMarked)
            {
                PointMarked();
                ResetRally();
            }
        }

        public void ResetRally()
        {
            rallyValue = 0;
        }

    }
}
