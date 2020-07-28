using Player;
using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// NCO
    /// </summary>
    public struct PlayerScore
    {
        public readonly string playerName;
        public bool isServing;
        public bool startsTheParty;
        public int markedPoints;
        public int markedGames;
        public int markedSets;

        public PlayerScore(PlayerID id)
        {
            playerName = id.name;
            isServing = false;
            startsTheParty = false;
            markedPoints = 0;
            markedGames = 0;
            markedSets = 0;
        }
    }

    /// <summary>
    /// NCO
    /// </summary>
    public class MatchDataComplete
    {
        #region Variables

        public int turnCount { get; private set; }
        public int gameCount { get; private set; }
        public int setCount { get; private set; }
        public readonly PlayerScore teamA;
        public readonly PlayerScore teamB;

        #endregion

        #region Constructors

        public MatchDataComplete(PlayerID teamA, PlayerID teamB)
        {
            this.teamA = new PlayerScore(teamA);
            this.teamB = new PlayerScore(teamB);
        }

        #endregion

        #region Methods

        public void AddTurn()
        {
            //we can add an analytics here
            turnCount++;
        }

        public void AddGame()
        {
            //we can add an analytics here
            gameCount++;
        }

        public void AddSet()
        {
            //we can add an analytics here
            setCount++;
        }

        #endregion
    }

}