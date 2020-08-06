using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace TennisMatch
{
    public class RallyScorer_Visual : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private RallyScorer rally;
        [Space(10)]
        [SerializeField] private RectTransform jeton;
        [SerializeField] private PartyScore score;

        [Header("Variable")]
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;
        [SerializeField] float[] jetonPose = new float[7];

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void OnEnable()
        {
            matchEvents.onExchange += OnExchange;
            matchEvents.onPointMarked += OnPointMarked;
            matchEvents.onGameMarked += OnGameMarked;
        }
        private void OnDisable()
        {
            matchEvents.onExchange -= OnExchange;
            matchEvents.onPointMarked -= OnPointMarked;
            matchEvents.onGameMarked -= OnGameMarked;
        }

        public void OnExchange(bool aTeamAction)
        {
            MoveToPos();
        }
        public void OnPointMarked(bool aTeamAction)
        {
            StopCoroutine(MoveToPosIn(moveDuration));
            StartCoroutine(MoveToPosIn(moveDuration));
        }
        private void OnGameMarked(bool aTeamAction)
        {
            StopCoroutine(MoveToPosIn(moveDuration));
            StopCoroutine(MoveToPosIn(moveDuration));
            StartCoroutine(MoveToPosIn(moveDuration));
        }
        private void MoveToPos()
        {
            // Pourquoi +3 ? Car rally value va de -3 à +3 et les pos du jetons de 0 à 7
            float targetPos = jetonPose[rally.value + 3];

            jeton.DOAnchorPosX(targetPos, moveDuration, false).SetEase(easeType);
        }

        IEnumerator MoveToPosIn(float duration)
        {
            yield return new WaitForSecondsRealtime(duration);

            MoveToPos();

            yield return null;
        }
    }
}
