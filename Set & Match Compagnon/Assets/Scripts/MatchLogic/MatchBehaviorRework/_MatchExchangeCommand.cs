using UnityEngine;
using System;

namespace TennisMatch
{
    /// <summary>
    /// ARD script 
    /// <para> 
    /// Informations comprises dans chaque échanges
    /// </para>
    /// </summary>
    [Serializable]
    public struct MatchExchange
    {
        public bool isService;

        public int playerShooting;
        public bool aTeamAction;

        public int rallyPosBeforeShoot;
        public int increment;

        public bool haveMarkedPoint;

        public MatchExchange
            (int playerShooting, bool isService,
             int rallyPos, int increment, bool pointMarked)
        {
            this.isService = isService;

            this.playerShooting = playerShooting;
            this.aTeamAction = playerShooting%2 == 0;

            this.rallyPosBeforeShoot = rallyPos;
            this.increment = increment;
            this.haveMarkedPoint = pointMarked;

        }
    }


    /// <summary>
    /// ARD script 
    /// <para> Créateur d'échange
    /// </para> </summary>
    public class _MatchExchangeCommand
    {
        public static MatchExchange GenerateExchange(int rallyPos, int increment, int playerTurn)
        {
            int turnOfPlayer = playerTurn;

            rallyPos = Mathf.Clamp(rallyPos, -3, 3);
            increment = turnOfPlayer % 2 == 0 ? increment : -increment;
            
            bool pointMarked = _MatchRally.PointWin(rallyPos + increment);

            //Construction de l'Exchange à save
            MatchExchange currentMove = new MatchExchange
            (turnOfPlayer, false,
             rallyPos, increment, pointMarked);

            return currentMove;
        }
    }
}
