using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD script
    /// </summary>
    public class MatchEndScreenTansition : MonoBehaviour
    {
        [Header("Game Meta")]
        [SerializeField] private MatchData match;

        [Header("Component")]
        [SerializeField] private RegularScreen matchScreen;
        [SerializeField] private RegularScreen endGameScreen;

        private void OnEnable()
        {
            MatchEvents.onMatchEnd += EndMatch;
        }
        private void OnDisable()
        {
            MatchEvents.onMatchEnd -= EndMatch;
        }

        private void EndMatch()
        {
            matchScreen.ExitViewportH(-2000);
            endGameScreen.SetActiveScreenFromH(2000);
        }
    }
}
