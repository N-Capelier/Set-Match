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
    public struct Exchange
    {
        public bool aTeamShoot;
        public bool isServiceShoot;

        public int rallyPosBeforeShoot;
        public int exchangePoints;

        public Score teamAScore;
        public Score teamBScore;

        public bool haveMarkedPoint;
        public bool haveMarkedGame;
        public bool haveMarkedSet;

        public Exchange(bool aTeamShoot, bool isService, int rallyPos, int exchangePoint,
                            Score teamAScore, Score teamBScore,
                                bool pointMarked, bool gameMarked, bool setMarked)
        {
            this.aTeamShoot = aTeamShoot;
            this.isServiceShoot = isService;

            this.rallyPosBeforeShoot = rallyPos;
            this.exchangePoints = exchangePoint;

            this.teamAScore = teamAScore;
            this.teamBScore = teamBScore;

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
