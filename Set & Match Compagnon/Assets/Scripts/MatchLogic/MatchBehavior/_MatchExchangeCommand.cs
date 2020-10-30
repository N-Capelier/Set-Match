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
        public bool is2ndService;

        public int playerShooting;
        public bool aTeamAction;

        public int rallyPosBeforeShoot;
        public int increment;

        public bool haveMarkedPoint;
        public bool haveFault;

        public MatchExchange
            (int playerShooting, bool isService, bool is2ndService,
             int rallyPos, int increment, bool pointMarked, bool haveFault)
        {
            this.isService = isService;
            this.is2ndService = is2ndService;

            this.playerShooting = playerShooting;
            aTeamAction = playerShooting%2 == 0;

            rallyPosBeforeShoot = rallyPos;
            this.increment = increment;

            haveMarkedPoint = pointMarked;
            this.haveFault = haveFault;
        }
    }


    /// <summary>
    /// ARD script 
    /// <para> Créateur d'échange
    /// </para> </summary>
    public static class _MatchExchangeCommand
    {
        public static MatchExchange GenerateExchange(int rallyPos, int increment, int playerTurn, bool isFault)
        {
            int turnOfPlayer = playerTurn;

            rallyPos = Mathf.Clamp(rallyPos, -3, 3);
            increment = turnOfPlayer % 2 == 0 ? increment : -increment;
            
            bool pointMarked = _MatchRally.PointWin(rallyPos + increment);

            //Construction de l'Exchange à save
            MatchExchange currentMove = new MatchExchange
            (turnOfPlayer, _MatchTurnManager.isService, _MatchTurnManager.is2ndService,
             rallyPos, increment, pointMarked, isFault);

            return currentMove;
        }
    }
}
