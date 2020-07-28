using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    [Serializable]
    public struct Score
    {
        public int point;
        public int[] gamePerSet;

        public Score(bool isEmpty)
        {
                point = 0;
                gamePerSet = new int[3] { 0, 0, 0 };
        }
        public Score(int pointValue, int GameWinOnSet1, int GameWinOnSet2, int GameWinOnSet3)
        {
            point = pointValue;
            gamePerSet = new int[3] { GameWinOnSet1, GameWinOnSet2, GameWinOnSet3 };
        }
    }

    /// <summary>
    /// ARD
    /// </summary>
    [CreateAssetMenu(fileName = "NewMatch", menuName = "MatchData")]
    public class MatchData : ScriptableObject
    {
        [Header("Team Informations")]
        public string teamA_Player1 = "AlphaPlayer";
        public string teamB_Player1 = "BetaPlayer";
        [Space(5)]
        //Match Simple ou Double
        public bool doubleMatch = false;
        [Space(5)]
        public string teamA_Player2 = "Alpha2Player";
        public string teamB_Player2 = "Beta2Player";
        [Space(25)]
        public Score teamA_Score = new Score(0, 0, 0, 0);
        public Score teamB_Score = new Score(0, 0, 0, 0);

        [Header("Match Informations")]
        [Range(1, 3)] public int currentSet = 1;
        [Range(1, 3)] public int MatchSetNumber = 3;
        [Space(10)]
        public bool teamA_StartServing = true;
        [Space(10)]
        public bool teamA_HaveService = true;
        [Space(10)]
        public bool teamA_Turn = true;
        [Space(10)]
        public int turnCount = 0;
        public int pointCount = 0;
        public int gameCount = 0;
        public int setCount = 0;

        /// <summary>
        /// Clear all match information to default 
        /// </summary>
        public void Reboot()
        {
            teamA_Player1 = "AlphaPlayer";
            teamB_Player1 = "BetaPlayer";
            doubleMatch = false;
            teamA_Player2 = "Alpha2Player";
            teamB_Player2 = "Beta2Player";

            teamA_Score = new Score(0, 0, 0, 0);
            teamB_Score = new Score(0, 0, 0, 0);

            currentSet = 1;
            MatchSetNumber = 3;

            teamA_StartServing = true;
            teamA_HaveService = true;
            teamA_Turn = true;

            turnCount = 0;
            pointCount = 0;
            gameCount = 0;
            setCount = 0;
        }
    }
}
