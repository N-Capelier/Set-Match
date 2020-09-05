using UnityEngine;

namespace TennisMatch
{
    public class MatchLoader : MonoBehaviour
    {
        [Header("Game Meta")]
        [SerializeField] public MatchData data;
        [SerializeField] public PlayerWaitingMatchs waitingMatchs;

        public void ButtonLoadMatch(int matchID)
        {
            waitingMatchs.LoadMatch(matchID, data);
            SceneHandler.GoToScene("ARD_LoadMatch");
        }
    }
}
