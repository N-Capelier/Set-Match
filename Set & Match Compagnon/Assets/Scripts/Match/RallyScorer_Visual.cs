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
        [Header("Component")]
        [SerializeField] private RectTransform jeton;
        [SerializeField] private PartyScore score;
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
            score.onTeamA_PointMarked += OnPointMarked;
            score.onTeamB_PointMarked += OnPointMarked;
        }
        private void OnDisable()
        {
            rally.onExchange -= OnExchange;
            score.onTeamA_PointMarked -= OnPointMarked;
            score.onTeamB_PointMarked -= OnPointMarked;

        }

        public void OnExchange()
        {
            MoveToPos();
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
    
        private void OnPointMarked()
        {
            rally.ResetRally();
             StartCoroutine(MoveToPosIn((rally.moveHistory.First().moveIncrement * 0.25f) + moveDuration));
        }

        IEnumerator MoveToPosIn(float duration)
        {
            yield return new WaitForSecondsRealtime(duration);

            MoveToPos();

            yield return null;
        }
    }
}
