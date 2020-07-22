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
        public string teamA_Player1 = "PlayerA1";
        public string teamB_Player1 = "PlayerB1";
        [Space(5)]
        //Match Simple ou Double
        public bool doubleMatch = false;
        [Space(5)]
        public string teamA_Player2 = "PlayerA2";
        public string teamB_Player2 = "PlayerB2";
        [Space(25)]
        public Score teamA_Score = new Score(true);
        public Score teamB_Score = new Score(true);

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


    }
}
