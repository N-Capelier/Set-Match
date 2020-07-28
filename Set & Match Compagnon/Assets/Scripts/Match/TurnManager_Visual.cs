using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class TurnManager_Visual : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private TurnManager turnManager;
        [SerializeField] private RallyScorer rallyScoreur;
        [SerializeField] private PartyScore partyScore;
        [Space(10)]
        [SerializeField] private RectTransform teamA;
        [SerializeField] private RectTransform teamB;

        [Header("Variable")]
        [SerializeField] private MatchData match;
        [Space(10)]
        [SerializeField] private Vector2 teamAPos;
        [SerializeField] private Vector2 centerPos;
        [SerializeField] private Vector2 teamBPos;
        [Space(10)]
        [SerializeField] private float centerScale = 1f;
        [SerializeField] private float spectScale = 0.5f;
        [Space(10)]
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void OnEnable()
        {
            matchEvents.onMatchStart += OnExchange;

            matchEvents.onExchange += OnExchange;
            matchEvents.onGameMarked += OnResetGame;
        }
        private void OnDisable()
        {
            matchEvents.onMatchStart -= OnExchange;

            matchEvents.onExchange -= OnExchange;
            matchEvents.onGameMarked -= OnResetGame;
        }

        public void OnExchange()
        {
            if (match.teamA_Turn)
            {
                TeamATurn();
            }
            else
            {
                TeamBTurn();
            }
        }
        private void OnResetGame()
        {
            if (match.teamA_HaveService)
            {
                TeamATurn();
            }
            else
            {
                TeamBTurn();
            }
        }

        private void TeamATurn()
        {
            teamA.DOAnchorPosX(centerPos.x, duration).SetEase(easeType);
            teamB.DOAnchorPosX(teamBPos.x, duration).SetEase(easeType);
            teamA.DOAnchorPosY(centerPos.y, duration).SetEase(easeType);
            teamB.DOAnchorPosY(teamBPos.y, duration).SetEase(easeType);

            teamA.DOScale(centerScale, duration).SetEase(easeType);
            teamB.DOScale(spectScale, duration).SetEase(easeType);
        }
        private void TeamBTurn()
        {
            teamA.DOAnchorPosX(teamAPos.x, duration).SetEase(easeType);
            teamB.DOAnchorPosX(centerPos.x, duration).SetEase(easeType); 
            teamA.DOAnchorPosY(teamAPos.y, duration).SetEase(easeType);
            teamB.DOAnchorPosY(centerPos.y, duration).SetEase(easeType);

            teamA.DOScale(spectScale, duration).SetEase(easeType);
            teamB.DOScale(centerScale, duration).SetEase(easeType);
        }

    }
}
