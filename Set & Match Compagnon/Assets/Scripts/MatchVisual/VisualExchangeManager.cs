using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class VisualExchangeManager : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private _MatchExchangeManager action;
        [Space(10)]
        [SerializeField] private Button UndoButton;
        [SerializeField] private TextMeshProUGUI UndoText;
        [SerializeField] private Image UndoImg;
        [Space(5)]
        [SerializeField] private Button RedoButton;
        [SerializeField] private TextMeshProUGUI RedoText;
        [SerializeField] private Image RedoImg;

        [Header("Variable")]
        [SerializeField] private float colorFadeDuration = 0.5f;
        [SerializeField] private Ease fadeMode = Ease.InOutCubic;

        private void Awake()
        {
            action = _MatchExchangeManager.Instance;
        }
        private void OnEnable()
        {
            MatchEvents.onMatchStart += UpdateVisual;
            MatchEvents.onVisualUpdate += UpdateVisual;
        }
        private void OnDisable()
        {
            MatchEvents.onMatchStart += UpdateVisual;
            MatchEvents.onVisualUpdate += UpdateVisual;
        }

        void UpdateVisual()
        {
            Invoke("ButtonColor", 0.1f);
        }

        private void ButtonColor()
        {
            if (action.canUndo)
            {
                UndoButton.interactable = true;
                UndoText.DOColor(Color.white, colorFadeDuration).SetEase(fadeMode);
                UndoImg.DOColor(Color.white, colorFadeDuration).SetEase(fadeMode);
            }
            else
            {
                UndoButton.interactable = false;
                UndoText.DOColor(Color.black, colorFadeDuration).SetEase(fadeMode);
                UndoImg.DOColor(Color.black, colorFadeDuration).SetEase(fadeMode);
            }

            if (action.canRedo)
            {
                RedoButton.interactable = true;
                RedoText.DOColor(Color.white, colorFadeDuration).SetEase(fadeMode);
                RedoImg.DOColor(Color.white, colorFadeDuration).SetEase(fadeMode);
            }
            else
            {
                RedoButton.interactable = false;
                RedoText.DOColor(Color.black, colorFadeDuration).SetEase(fadeMode);
                RedoImg.DOColor(Color.black, colorFadeDuration).SetEase(fadeMode);

            }
        }
    }
}
