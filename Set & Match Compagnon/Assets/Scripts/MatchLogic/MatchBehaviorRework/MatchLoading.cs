using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class MatchLoading : MonoBehaviour
    {
        [Header("GameMeta")]
        private MatchEvents matchEvents;
        [SerializeField] private MatchData match;

        [Header("Component")]
        [SerializeField] private _MatchExchangeManager exchange;
        [Space(10)]
        [SerializeField] private RectTransform jeton;

        private void Awake()
        {
            matchEvents = MatchEvents.Instance;
            matchEvents.VisualUpdate();
        }

        private void Start()
        {
            matchEvents.VisualUpdate();
        }
    }
}

