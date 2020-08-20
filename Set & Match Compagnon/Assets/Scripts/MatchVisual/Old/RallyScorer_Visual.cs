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

        [Header("Variable")]
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.InOutCubic;
        [SerializeField] float[] jetonPose = new float[7];

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void OnEnable()
        {
            matchEvents.onVisualUpdate += UpdateVisual;
        }
        private void OnDisable()
        {
            matchEvents.onVisualUpdate -= UpdateVisual;
        }

        public void UpdateVisual()
        {
            MoveToPosIn(0.6f);
            MoveToPos();
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
