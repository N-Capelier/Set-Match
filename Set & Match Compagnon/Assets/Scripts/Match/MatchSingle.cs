using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    [CreateAssetMenu(fileName = "MatchSingle", menuName = "Match/Single")]
    public class MatchSingle : ScriptableObject
    {
        #region Variables

        public readonly PlayerScore playerA;
        public readonly PlayerScore playerB;
        public int turnCount;
        public int gameCount;
        public int setCount;

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
