using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStats
    {
        #region Variables

        float playerScore = 0;
        float pressureCoeff = 0;
        int victoryCount = 0;
        int startGameService = 0;
        int gamesPlayed = 0;
        int hitToScore = 0;
        int hitCount = 0;

        #endregion

        #region Constructors

        public PlayerStats()
        {
            
        }

        public PlayerStats(float playerScore, float pressureCoeff, int victoryCount, int startGameService,
            int gamesPlayed, int hitToScore, int hitCount)
        {
            this.playerScore = playerScore;
            this.pressureCoeff = pressureCoeff;
            this.victoryCount = victoryCount;
            this.startGameService = startGameService;
            this.gamesPlayed = gamesPlayed;
            this.hitToScore = hitToScore;
            this.hitCount = hitCount;
        }

        #endregion

        #region Methods

        public PlayerStats Share()
        {
            return new PlayerStats(playerScore, pressureCoeff, victoryCount, startGameService,
                gamesPlayed, hitToScore, hitCount);
        }

        public float VictoryRatio()
        {
            return victoryCount / gamesPlayed;
        }

        public float HitRatio()
        {
            return hitToScore / hitCount;
        }

        public float StartServiceRatio()
        {
            return startGameService / gamesPlayed;
        }

        #endregion
    }

}