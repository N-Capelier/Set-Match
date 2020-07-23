using UnityEngine;
using DG.Tweening;
using System.Linq;
using DG.Tweening.Core;
using UnityEngine.UI;
using System.Collections;

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

        public void OnExchange()
        {
            MoveToPos();
        }
        public void OnPointMarked()
        {
            StopCoroutine(MoveToPosIn(moveDuration));
            StartCoroutine(MoveToPosIn(moveDuration));
        }
        private void OnGameMarked()
        {
            StopCoroutine(MoveToPosIn(moveDuration));
            StopCoroutine(MoveToPosIn((rally.moveHistory.First().moveIncrement * 0.25f) + moveDuration));
            StartCoroutine(MoveToPosIn((rally.moveHistory.First().moveIncrement * 0.25f) + moveDuration));
        }
        private void MoveToPos()
        {
            /// <summary>
            /// Pourquoi +3 ?
            /// Car rally value va de -3 à +3 et les pos du jetons de 0 à 7
            /// </summary>
            float targetPos = jetonPose[rally.rallyValue + 3];
            Move lastMove = rally.moveHistory.First();

            jeton.DOAnchorPosX(targetPos, Mathf.Abs(lastMove.moveIncrement * 0.25f) + moveDuration, false).SetEase(easeType);
        }

        IEnumerator MoveToPosIn(float duration)
        {
            yield return new WaitForSecondsRealtime(duration);

            MoveToPos();

            yield return null;
        }
    }
}
