using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TennisMatch;
using UnityEngine.Timeline;

namespace Player
{
    /// <summary>
    /// NCO
    /// </summary>
    public class PlayerData
    {
        MatchDataUnfinished[] currentMatches = new MatchDataUnfinished[20];

        #region Methods

        public MatchDataUnfinished LoadMatch(int matchID)
        {
            return currentMatches[matchID];
        }

        public void DeleteMatch(int matchID)
        {
            for(int i = matchID; i < currentMatches.Length; i++)
            {
                if(i + 1 < currentMatches.Length)
                {
                    currentMatches[i] = currentMatches[i + 1];
                }
                else
                {
                    currentMatches[i] = null;
                }
            }
        }

        public void AddMatch(MatchDataUnfinished match)
        {
            //Look for empty space
            for(int i = 0; i < currentMatches.Length; i++)
            {
                if(currentMatches[i] == null)
                {
                    currentMatches[i] = match;
                    return;
                }
            }

            //if no empty space, move all index to n+1 and add the new match to position 0
            for(int i = currentMatches.Length - 1; i >= 0; i--)
            {
                currentMatches[i] = currentMatches[i - 1];
            }
            currentMatches[0] = match;
        }

        #endregion
    }

}