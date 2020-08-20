using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD script
    /// </summary>
    public class MatchEndScreenTansition : MonoBehaviour
    {
        [Header("Game Meta")]
        private MatchEvents matchEvents;
        [SerializeField] private MatchData match;

        [Header("Component")]
        [SerializeField] private RegularScreen matchScreen;
        [SerializeField] private RegularScreen endGameScreen;

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void OnEnable()
        {
            matchEvents.onMatchEnd += EndMatch;
        }
        private void OnDisable()
        {
            matchEvents.onMatchEnd -= EndMatch;
        }

        private void EndMatch()
        {
            matchScreen.ExitViewportH(-2000);
            endGameScreen.SetActiveScreenFromH(2000);
        }
    }
}
