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
        [Header("Component"), SerializeField]
        private RectTransform jeton;
        [SerializeField] private Button scoreFocus, scoreUnfocus;
        private RallyScorer rally;

        [Header("Variable")]
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;
        [SerializeField] float[] jetonPose = new float[7];

        private void Awake()
        {
            rally = RallyScorer.Instance;
        }

        private void OnEnable()
        {
            rally.onExchange += OnExchange;
            rally.onPointMarked += OnPoint;
        }
        private void OnDisable()
        {
            rally.onExchange -= OnExchange;
            rally.onPointMarked -= OnPoint;
        }


        public void OnExchange()
        {
            MoveToPos();
        }

        public void OnPoint()
        {
            StopAllCoroutines();
            StartCoroutine(ScoreTemporaryFocus(2f));
        }

        IEnumerator ScoreTemporaryFocus(float duration)
        {
            scoreFocus.onClick?.Invoke();

            yield return new WaitForSecondsRealtime(duration * 0.5f);

            MoveToPos();

            yield return new WaitForSecondsRealtime(duration * 0.5f);

            scoreUnfocus.onClick?.Invoke();

            yield return null;
        }

        private void MoveToPos()
        {
            /// <summary>
            /// Pourquoi +3 ?
            /// Car rally value va de -3 à +3 et les pos du jetons de 0 à 7
            /// </summary>
            float targetPos = jetonPose[rally.rallyValue + 3];
            Move lastMove = rally.moveHistory.First();

            jeton.DOAnchorPosX(targetPos, Mathf.Abs(lastMove.moveIncrement * 0.5f) * moveDuration, false).SetEase(easeType);
        }
    }
}
