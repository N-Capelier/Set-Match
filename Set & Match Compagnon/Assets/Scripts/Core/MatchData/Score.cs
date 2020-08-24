using System.Collections.Generic;
using UnityEngine;
using System;

namespace TennisMatch
{
    /// <summary> ARD script
    /// <list type="explication">
    /// <item> aTeamSets  </item>
    /// <item> bTeamSets  </item>
    /// <item> actualSet  </item>
    /// <item> List Sets  </item>
    /// </list> </summary>
    [Serializable]
    public class Score
    {
        public int aTeamSets = 0;
        public int bTeamSets = 0;

        [Range(1, 3)] public int MatchSetNumber = 3;
        [Range(0, 2)] public int actualSet = 0;
        public List<Set> Sets = new List<Set>();
    }

    /// <summary> ARD script
    /// <list type="explication">
    /// <item> aTeamGames  </item>
    /// <item> bTeamGames  </item>
    /// <item> actualGame  </item>
    /// <item> List Games  </item>
    /// </list> </summary>
    [Serializable]
    public class Set
    {
        public int aTeamGames = 0;
        public int bTeamGames = 0;

        public bool setEnd = false;
        public bool aTeamWinSet = false;

        public int actualGame = 0;
        public List<Game> Games = new List<Game>();
    }

    /// <summary> ARD script
    /// <list type="explication">
    /// <item> aTeamPoint                          </item>
    /// <item> bTeamPoint                          </item>
    /// <item> gameEnd = does the game is finished </item>
    /// <item> aTeamGame = game win by team A      </item>
    /// </list> </summary>
    [Serializable] 
    public class Game
    {
        public int aTeamPoint = 0;
        public int bTeamPoint = 0;

        public bool gameEnd = false;
        public bool aTeamWinGame = false;

        public Game()
        {
            aTeamPoint = 0;
            bTeamPoint = 0;

            gameEnd = false;
            aTeamWinGame = false;
        }
        public Game(int aTeamPoint, int bTeamPoint)
        {
            this.aTeamPoint = aTeamPoint;
            this.bTeamPoint = bTeamPoint;

            gameEnd = false;
            aTeamWinGame = false;
        }
        public Game(int aTeamPoint, int bTeamPoint, bool aTeamWinTheGame)
        {
            this.aTeamPoint = aTeamPoint;
            this.bTeamPoint = bTeamPoint;

            gameEnd = true;
            aTeamWinGame = aTeamWinTheGame;
        }
    }

    /// <summary>
    /// ARD script 
    /// <para> 
    /// Informations comprises dans chaque échanges
    /// </para>
    /// </summary>
    [Serializable]
    public struct FinalScore
    {
        public bool aTeamWinMatch;

        public int aTeamSets;
        public int bTeamSets;

        public SetComplete[] sets;

        public FinalScore(SetComplete[] sets)
        {
            this.sets = sets;

            aTeamSets = 0;
            bTeamSets = 0;

            foreach (SetComplete set in sets)
            {
                if (set.aTeamWinSet)
                {
                    aTeamSets++;
                }
                else
                {
                    bTeamSets++;
                }
            }

            aTeamWinMatch = aTeamSets > bTeamSets;
        }

    }


    /// <summary>
    /// ARD script 
    /// <para> 
    /// Informations comprises dans chaque échanges
    /// </para>
    /// </summary>
    [Serializable]
    public struct SetComplete
    {
        public bool aTeamWinSet;

        public int aTeamGames;
        public int bTeamGames;

        public GameComplete[] games;

        public SetComplete(GameComplete[] games)
        {
            this.games = games;

            aTeamGames = 0;
            bTeamGames = 0;

            foreach(GameComplete game in games)
            {
                if (game.aTeamWinGame)
                {
                    aTeamGames++;
                }
                else
                {
                    bTeamGames++;
                }
            }

            aTeamWinSet = aTeamGames > bTeamGames;
        }

    }

    /// <summary>
    /// ARD script 
    /// <para> 
    /// Informations comprises dans chaque échanges
    /// </para>
    /// </summary>
    [Serializable]
    public struct GameComplete
    {
        public bool aTeamWinGame;

        public int aTeamPoints;
        public int bTeamPoints;

        public GameComplete(int aTeamPoints, int bTeamPoints)
        {
            this.aTeamPoints = aTeamPoints;
            this.bTeamPoints = bTeamPoints;

            aTeamWinGame = aTeamPoints > bTeamPoints;
        }
    }
}
