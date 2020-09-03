using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    [CreateAssetMenu(fileName = "NewWaitingMatchs", menuName = "WaitingMatchs")]

    public class PlayerWaitingMatchs : ScriptableObject
    {
        [Header("Game Meta")]
        [SerializeField] public UnfinnishedMatch[] matchs = new UnfinnishedMatch[10];

        #region Methods
        public void LoadMatch(int matchID, MatchData data)
        {
            if (matchs[matchID - 1] != null)
            {
                return;
            }

            data.LoadMatch(matchs[matchID - 1]);

            SceneHandler.GoToScene("ARD_LoadMatch");
        }

        public void DeleteUnfinnishedMatch(int matchID)
        {
            for (int i = matchID; i < matchs.Length; i++)
            {
                if (i + 1 < matchs.Length)
                {
                    matchs[i] = matchs[i + 1];
                }
                else
                {
                    matchs[i] = null;
                }
            }
        }
        public void AddUnfinnishedMatch(UnfinnishedMatch match)
        {
            //Look for empty space
            for (int i = 0; i < matchs.Length; i++)
            {
                if (matchs[i] == null)
                {
                    matchs[i] = match;
                    return;
                }
            }

            //if no empty space, move all index to n+1 and add the new match to position 0
            for (int i = matchs.Length - 1; i > 0; i--)
            {
                matchs[i] = matchs[i - 1];
            }
            matchs[0] = match;
        }

        #endregion
    }
}
