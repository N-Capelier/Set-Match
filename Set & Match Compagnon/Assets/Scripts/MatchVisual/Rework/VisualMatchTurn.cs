using UnityEngine;
using DG.Tweening;

namespace TennisMatch
{
    /// <summary>
    /// ARD script
    /// </summary>
    public class VisualMatchTurn : MonoBehaviour
    {
        [Header("Game Meta")]
        private MatchEvents matchEvents;
        [SerializeField] private MatchData match;

        [Header("Component")]
        [SerializeField] private _MatchTurnManager turnManager;
        [Space(15)]
        [SerializeField] private RectTransform teamA1;
        [SerializeField] private RectTransform teamB1;
        [Space(5)]
        [SerializeField] private RectTransform teamA2;
        [SerializeField] private RectTransform teamB2;

        [Header("Positions")]
        [SerializeField] private Vector2 centerPos;
        [Space(5)]
        [SerializeField] private Vector2 teamAPos;
        [SerializeField] private Vector2 teamBPos;
        [Space(10)]
        [SerializeField] private Vector2 teamASecondaryPos;
        [SerializeField] private Vector2 teamBSecondaryPos;
        [Space(10)]

        [Header("Variable")]
        [SerializeField] private float centerScale = 1f;
        [SerializeField] private float spectScale = 0.5f;
        [Space(10)]
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void OnEnable()
        {
            matchEvents.onMatchStart += UpdateTurn;
            matchEvents.onVisualUpdate += UpdateTurn;
        }
        private void OnDisable()
        {
            matchEvents.onMatchStart -= UpdateTurn;
            matchEvents.onVisualUpdate -= UpdateTurn;
        }

        public void UpdateTurn()
        {
            TurnOf(turnManager.turnOfPlayer);
        }
        private void TurnOf(int turnOfPlayer)
        {
            RectTransform centerPlayer = null;

            RectTransform specPlayer = null;
            Vector2 specPos = Vector2.zero;

            if (turnManager.matchPlayers.Count == 2)
            {
                teamA2.gameObject.SetActive(false);
                teamB2.gameObject.SetActive(false);

                switch (turnOfPlayer)
                {
                    case 0:
                        centerPlayer = teamA1;
                        specPlayer = teamB1;

                        specPos = teamBPos;
                        break;
                    case 1:
                        centerPlayer = teamB1;
                        specPlayer = teamA1;

                        specPos = teamAPos;
                        break;
                }

                centerPlayer.DOAnchorPosX(centerPos.x, duration).SetEase(easeType);
                centerPlayer.DOAnchorPosY(centerPos.y, duration).SetEase(easeType);
                centerPlayer.DOScale(centerScale, duration).SetEase(easeType);

                specPlayer.DOAnchorPosX(specPos.x, duration).SetEase(easeType);
                specPlayer.DOAnchorPosY(specPos.y, duration).SetEase(easeType);
                specPlayer.DOScale(spectScale, duration).SetEase(easeType);
            }
            else
            if (turnManager.matchPlayers.Count == 4)
            {
                RectTransform specPlayer2 = null;
                Vector2 specPos2 = Vector2.zero;

                RectTransform specPlayer3 = null;
                Vector2 specPos3 = Vector2.zero;

                switch (turnOfPlayer)
                {
                    case 0:
                        centerPlayer = teamA1;

                        specPlayer = teamB1;
                        specPos = teamBPos;

                        specPlayer2 = teamA2;
                        specPos2 = teamAPos;

                        specPlayer3 = teamB2;
                        specPos3 = teamBSecondaryPos;
                        break;

                    case 1:
                        centerPlayer = teamB1;

                        specPlayer = teamA2;
                        specPos = teamAPos;

                        specPlayer2 = teamB2;
                        specPos2 = teamBPos;

                        specPlayer3 = teamA1;
                        specPos3 = teamASecondaryPos;
                        break;

                    case 2:
                        centerPlayer = teamA2;

                        specPlayer = teamB2;
                        specPos = teamBPos;

                        specPlayer2 = teamA1;
                        specPos2 = teamAPos;

                        specPlayer3 = teamB1;
                        specPos3 = teamBSecondaryPos;
                        break;

                    case 3:
                        centerPlayer = teamB2;

                        specPlayer = teamA1;
                        specPos = teamAPos;

                        specPlayer2 = teamB1;
                        specPos2 = teamBPos;

                        specPlayer3 = teamA2;
                        specPos3 = teamASecondaryPos;
                        break;
                }

                centerPlayer.DOAnchorPosX(centerPos.x, duration).SetEase(easeType);
                centerPlayer.DOAnchorPosY(centerPos.y, duration).SetEase(easeType);
                centerPlayer.DOScale(centerScale, duration).SetEase(easeType);

                specPlayer.DOAnchorPosX(specPos.x, duration).SetEase(easeType);
                specPlayer.DOAnchorPosY(specPos.y, duration).SetEase(easeType);
                specPlayer.DOScale(spectScale, duration).SetEase(easeType);

                specPlayer2.DOAnchorPosX(specPos2.x, duration).SetEase(easeType);
                specPlayer2.DOAnchorPosY(specPos2.y, duration).SetEase(easeType);
                specPlayer2.DOScale(spectScale, duration).SetEase(easeType);

                specPlayer3.DOAnchorPosX(specPos3.x, duration).SetEase(easeType);
                specPlayer3.DOAnchorPosY(specPos3.y, duration).SetEase(easeType);
                specPlayer3.DOScale(spectScale, duration).SetEase(easeType);

            }
            else
            {
                Debug.LogError("");
            }
        }
    }
}
