using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace TennisMatch
{
    public class VisualMatchRally : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private _MatchExchangeManager exchange;
        [Space(10)]
        [SerializeField] private RectTransform jeton;

        [Header("Variable")]
        [SerializeField, Range(0, 0.3f)] private float reductDistFactor = 0.2f;
        [SerializeField, Range(0.5f, 1.5f)] private float baseMoveDur = 0.5f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;
        [SerializeField] float[] jetonPose = new float[7];

        private void Awake() => matchEvents = MatchEvents.Instance;
        private void OnEnable() => matchEvents.onVisualUpdate += UpdateVisual;
        private void OnDisable() => matchEvents.onVisualUpdate -= UpdateVisual;
        
        public void UpdateVisual()
        {
            MatchExchange lastMove = exchange.moveHistory.Peek();

            int rallyPos = lastMove.rallyPosBeforeShoot + lastMove.increment;
            int ballDistance = Mathf.Abs(lastMove.increment);

            // Pourquoi +3 ? Car rally value va de -3 à +3 et les pos du jetons de 0 à 7
            float targetPos = jetonPose[rallyPos + 3];

            jeton.DOAnchorPosX(targetPos, baseMoveDur - (reductDistFactor * ballDistance), false).SetEase(easeType);

            if(Mathf.Abs(rallyPos) == 3)
            {
                StopCoroutine(PointWin(targetPos, ballDistance));
                StartCoroutine(PointWin(targetPos, ballDistance));
            }
        }
        
        IEnumerator PointWin(float targetPos, int shootDist)
        {
            jeton.DOAnchorPosX(targetPos, baseMoveDur - (reductDistFactor * shootDist), false).SetEase(easeType);

            yield return new WaitForSecondsRealtime(shootDist);

            jeton.DOAnchorPosX(3, baseMoveDur - (reductDistFactor * 3), false).SetEase(easeType);

            yield return null;
        }
    }
}
