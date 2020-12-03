using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace TennisMatch
{
    public class VisualMatchRally : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private _MatchExchangeManager exchange;
        [SerializeField] private _MatchRally rally;
        [Space(10)]
        [SerializeField] private RectTransform jeton;
        [SerializeField] private TextMeshProUGUI BonusText;

        [Header("Variable")]
        [SerializeField, Range(0, 0.3f)] private float reductDistFactor = 0.2f;
        [SerializeField, Range(0.5f, 1.5f)] private float baseMoveDur = 0.5f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;
        [SerializeField] float[] jetonPose = new float[7];

        private void OnEnable() => MatchEvents.onVisualUpdate += UpdateVisual;
        private void OnDisable() => MatchEvents.onVisualUpdate -= UpdateVisual;

        private void FixedUpdate()
        {
            BonusText.gameObject.SetActive(rally.Bonus >= 0);
            BonusText.text = rally.Bonus.ToString();
        }

        public void UpdateVisual()
        {
            if (exchange.moveHistory.Count > 0)
            {
                MatchExchange lastMove = exchange.moveHistory.Peek();

                int rallyPos = lastMove.rallyPosBeforeShoot + lastMove.increment;
                int ballDistance = Mathf.Abs(lastMove.increment) <= 3 ? Mathf.Abs(lastMove.increment) : 3 ;
                float targetPos = 0;

                if (lastMove.haveFault)
                {
                    targetPos = 3;
                }
                else
                {
                    rallyPos = Mathf.Clamp(rallyPos, -3, 3);
                    // Pourquoi +3 ? Car rally value va de -3 à +3 et les pos du jetons de 0 à 7
                    targetPos = jetonPose[rallyPos + 3];
                }

                jeton.DOAnchorPosX(targetPos, baseMoveDur - (reductDistFactor * ballDistance), false).SetEase(easeType);

                if (Mathf.Abs(rallyPos) == 3)
                {
                    StopCoroutine(PointWin(targetPos, ballDistance));
                    StartCoroutine(PointWin(targetPos, ballDistance));
                }
            }
            else
            {
                jeton.DOAnchorPosX(jetonPose[3], baseMoveDur, false).SetEase(easeType);
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
