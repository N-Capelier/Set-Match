using UnityEngine;

namespace TennisMatch
{
    public class MatchLoading : MonoBehaviour
    {
        private void Awake()
        {
            MatchEvents.VisualUpdate();
        }

        private void Start()
        {
            MatchEvents.VisualUpdate();
        }
    }
}

