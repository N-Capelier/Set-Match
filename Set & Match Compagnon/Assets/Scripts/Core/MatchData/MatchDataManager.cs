using System;
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
    public struct CompleteMatch
    {
        [Header("Teams Informations")]
        public string myPlayer_Name;
        public Sprite myPlayer_Sprite;
        public string advPlayer1_Name ;
        public Sprite advPlayer1_Sprite;
        [Space(5)]
        public bool doubleMatch;
        [Space(5)]
        public string alliePlayer_Name;
        public Sprite alliePlayer_Sprite;
        public string advPlayer2_Name;
        public Sprite advPlayer2_Sprite;

        [Header("Match Informations")]
        public bool playerTeamWin;
        public int matchSetNumber;

        [Header("Match Data")]
        public FinalScore score;
        public int turnCount;
        public int pointCount;
        public int gameCount;

        public CompleteMatch(MatchData data)
        {
            #region Teams Informations

            myPlayer_Name = data.teamA_Player1;
            myPlayer_Sprite = data.teamA_P1img;
            advPlayer1_Name = data.teamB_Player1;
            advPlayer1_Sprite = data.teamB_P1img;

            doubleMatch = data.doubleMatch;

            alliePlayer_Name = data.teamA_Player2;
            alliePlayer_Sprite = data.teamA_P2img;
            advPlayer2_Name = data.teamB_Player2;
            advPlayer2_Sprite = data.teamB_P2img;

            #endregion

            #region MatchData

            //Array de Set
            int setNumber = data.score.Sets.Count;
            SetComplete[] sets = new SetComplete[setNumber];
            for (int set = 0; set < data.score.Sets.Count; set++)
            {
                //Array de Game
                int thisSetGameNumber = data.score.Sets[set].Games.Count;
                GameComplete[] gamesSaved = new GameComplete[thisSetGameNumber];
                for (int game = 0; game < data.score.Sets.Count; game++)
                {
                    //Récupération des info de chaque jeu de chaque set
                    int thisGameOfThisSet_aTeamPoints = data.score.Sets[set].Games[game].aTeamPoint;
                    int thisGameOfThisSet_bTeamPoints = data.score.Sets[set].Games[game].aTeamPoint;

                    //Game Renseignée
                    gamesSaved[game] = new GameComplete(thisGameOfThisSet_aTeamPoints, thisGameOfThisSet_bTeamPoints);
                }

                //Set Renseignée
                sets[set] = new SetComplete(gamesSaved);
            }
            //Score Renseignée
            FinalScore savedScore = new FinalScore(sets);

            //score sauvegardé
            score = savedScore;

            #endregion

            //Match Informations
            turnCount = data.turnCount;
            pointCount = data.pointCount;
            gameCount = data.gameCount;

            playerTeamWin = score.aTeamWinMatch;
            matchSetNumber = score.sets.Length;
        }
    }

    /// <summary>
    /// ARD script 
    /// <para> 
    /// Informations comprises dans chaque échanges
    /// </para>
    /// </summary>
    [Serializable]
    public struct UnfinnishedMatch
    {
        [Header("Team Informations")]
        public string teamA_Player1;
        public string teamB_Player1;
        public Sprite teamA_P1img;
        public Sprite teamB_P1img;
        [Space(5)]
        //Match Simple ou Double
        public bool doubleMatch;
        [Space(5)]
        public string teamA_Player2;
        public string teamB_Player2;
        public Sprite teamA_P2img;
        public Sprite teamB_P2img;

        [Header("Match score")]
        public Score score;

        [Header("Match Informations")]
        public bool matchEnd;
        public bool teamA_WinMatch;
        [Space(10)]
        public bool teamA_StartGame;
        public bool teamA_HaveService;
        public bool teamA_Turn;
        [Space(10)]
        public int turnCount;
        public int pointCount;
        public int gameCount;

        public UnfinnishedMatch(MatchData data)
        {
          teamA_Player1 = data.teamA_Player1;
          teamB_Player1 = data.teamB_Player1;
          teamA_P1img = data.teamA_P1img; ;
          teamB_P1img = data.teamB_P1img; ;

          doubleMatch = data.doubleMatch; ;
 
          teamA_Player2 = data.teamA_Player2; ;
          teamB_Player2 = data.teamB_Player2; ;
          teamA_P2img = data.teamA_P2img; ;
          teamB_P2img = data.teamB_P2img; ;

          score = data.score;

          matchEnd = data.matchEnd;
          teamA_WinMatch = data.teamA_WinMatch;

          teamA_StartGame = data.teamA_StartGame;
          teamA_HaveService = data.teamA_HaveService;
          teamA_Turn = data.teamA_Turn;

          turnCount = data.turnCount;
          pointCount = data.pointCount;
          gameCount = data.gameCount;
    }
    }

    /// <summary>
    /// ARD
    /// </summary>
    public class MatchDataConvertor
    {
        public static CompleteMatch ConvertToCompleteMatch(MatchData data)
        {
            CompleteMatch match = new CompleteMatch(data);
            return match;
        }

        public static UnfinnishedMatch ConvertToUnfinnishedMatch(MatchData data)
        {
            UnfinnishedMatch match = new UnfinnishedMatch(data);
            return match;
        }
    }
}
