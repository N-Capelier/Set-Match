using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD script 
    /// <para> 
    /// Informations comprises dans chaque échanges
    /// </para>
    /// </summary>
    [Serializable]
    public struct Exchange
    {
        public bool aTeamShoot;
        public bool isServiceShoot;

        public int rallyPosBeforeShoot;
        public int exchangePoints;

        public bool haveMarkedPoint;
        public bool haveMarkedGame;
        public bool haveMarkedSet;

        public Exchange(bool aTeamShoot, bool isService, int rallyPos, int exchangePoint,
                         bool pointMarked, bool gameMarked, bool setMarked)
        {
            this.aTeamShoot = aTeamShoot;
            this.isServiceShoot = isService;

            this.rallyPosBeforeShoot = rallyPos;
            this.exchangePoints = exchangePoint;

            this.haveMarkedPoint = pointMarked;
            this.haveMarkedGame = gameMarked;
            this.haveMarkedSet = setMarked;
        }
    }

    /// <summary>
    /// ARD
    /// </summary>
    public class ExchangeCommand
    {
        public Exchange info = new Exchange();

        public void Execute()
        {

        }

        public void Undo()
        {

        }

    }
}
