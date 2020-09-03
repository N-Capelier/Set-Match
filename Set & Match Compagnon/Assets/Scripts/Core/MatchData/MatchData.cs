using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    [CreateAssetMenu(fileName = "NewMatch", menuName = "MatchData")]
    public class MatchData : ScriptableObject
    {
        [Header("Team Informations")]
        public string teamA_Player1 = "P1";
        public string teamB_Player1 = "P2";
        public Sprite teamA_P1img = null;
        public Sprite teamB_P1img = null;
        [Space(5)]
        //Match Simple ou Double
        public bool doubleMatch = false;
        [Space(5)]
        public string teamA_Player2 = "P3";
        public string teamB_Player2 = "P4";
        public Sprite teamA_P2img = null;
        public Sprite teamB_P2img = null;

        [Header("Match score")]
        public Score score = null;

        [Header("Match Informations")]
        public bool matchEnd = false;
        public bool teamA_WinMatch = false;
        [Space(10)]
        public bool teamA_StartGame = true;
        public bool teamA_HaveService = true;
        public bool teamA_Turn = true;
        [Space(10)]
        public int turnCount = 0;
        public int pointCount = 0;
        public int gameCount = 0;

        /// <summary>
        /// Clear all match information to default 
        /// </summary>
        public void Reboot()
        {
            teamA_Player1 = "P1";
            teamB_Player1 = "P2";
            doubleMatch = false;
            teamA_Player2 = "P3";
            teamB_Player2 = "P4";

            score = new Score();

            matchEnd = false;
            teamA_WinMatch = false;

            teamA_StartGame = true;
            teamA_HaveService = true;
            teamA_Turn = true;

            turnCount = 0;
            pointCount = 0;
            gameCount = 0;
        }
        public void LoadMatch(UnfinnishedMatch matchToContinue)
        {
            teamA_Player1 = matchToContinue.teamA_Player1;
            teamB_Player1 = matchToContinue.teamB_Player1;
            doubleMatch = matchToContinue.doubleMatch;
            teamA_Player2 = matchToContinue.teamA_Player2;
            teamB_Player2 = matchToContinue.teamB_Player2;

            score = matchToContinue.score;

            matchEnd = matchToContinue.matchEnd;
            teamA_WinMatch = matchToContinue.teamA_WinMatch;

            teamA_StartGame = matchToContinue.teamA_StartGame;
            teamA_HaveService = matchToContinue.teamA_HaveService;
            teamA_Turn = matchToContinue.teamA_Turn;

            turnCount = matchToContinue.turnCount;
            pointCount = matchToContinue.pointCount;
            gameCount = matchToContinue.gameCount;
        }

    }
}
