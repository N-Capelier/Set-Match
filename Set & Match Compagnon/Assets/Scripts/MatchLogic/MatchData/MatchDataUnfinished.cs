using Player;
using UnityEngine;

namespace TennisMatch
{
    public class MatchDataUnfinished : MonoBehaviour
    {
        #region Variables

        public int turnCount { get; private set; }
        public int gameCount { get; private set; }
        public int setCount { get; private set; }
        public readonly PlayerScore teamA;
        public readonly PlayerScore teamB;

        #endregion

        #region Constructors

        public MatchDataUnfinished(PlayerID teamA, PlayerID teamB)
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
