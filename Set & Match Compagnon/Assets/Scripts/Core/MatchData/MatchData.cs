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

            score = new Score();

            matchEnd = false;
            teamA_WinMatch = false;

            teamA_StartGame = true;
            teamA_HaveService = true;
            teamA_Turn = true;

            turnCount = 0;
            pointCount = 0;
            gameCount = 0;
            setCount = 0;
        }
    }
}
