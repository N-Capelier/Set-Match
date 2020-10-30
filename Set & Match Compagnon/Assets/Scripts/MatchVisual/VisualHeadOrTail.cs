using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace TennisMatch
{
    /// <summary>
    /// ARD script
    /// </summary>
    public class VisualHeadOrTail : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private _MatchHeadOrTail headOrTail;
        [Space(15)]
        [SerializeField] private Button startTheGame;
        [SerializeField] private RectTransform button;
        [Space(5)]
        [SerializeField] private RectTransform coin;
        [SerializeField] private TextMeshProUGUI coinText;

        [Header("Variable")]
        [SerializeField] private float launchDuration = 2f;
        [SerializeField] private Ease rotation = Ease.InOutCubic;
        [SerializeField, Range(1, 24)] private int flipNumber = 6;
        private bool haveAnimate = false;

        public void OnCoinLauch()
        {
            if (headOrTail.aTeamChooseHead != headOrTail.bTeamChooseHead)
            {
                if (headOrTail.coinhaveBeenLauch && !haveAnimate)
                {
                    StopCoroutine(CoinFlipping(launchDuration, flipNumber));
                    StartCoroutine(CoinFlipping(launchDuration, flipNumber));

                    haveAnimate = true;
                }
                else
                {
                    StopCoroutine(ButtonShowcase(launchDuration * 0.5f));
                    StartCoroutine(ButtonShowcase(launchDuration * 0.5f));
                }
            }
            else
            {
                CoinError(launchDuration * 0.5f);
            }
        }

        private void CoinError(float duration)
        {
            coin.DOShakeAnchorPos(duration, 20, 20);
        }
        IEnumerator ButtonShowcase(float duration)
        {
            button.DOJumpAnchorPos(button.anchoredPosition, 20, 1, duration);

            button.DOScale(1.5f, duration * 0.5f).SetEase(rotation);

            yield return new WaitForSecondsRealtime(duration * 0.5f);

            button.DOScale(1, duration * 0.5f).SetEase(rotation);
        }
        IEnumerator CoinFlipping(float duration, int flipIteration)
        {
            coinText.text = "   ";

            coin.DORotate(new Vector3(flipIteration * 360, 0, 0), duration, RotateMode.FastBeyond360).SetEase(rotation);
            coin.DOMoveY(coin.position.y + 200, duration * 0.5f).SetEase(rotation);
            coin.DOScale(2, duration * 0.5f).SetEase(rotation);

            yield return new WaitForSecondsRealtime(duration * 0.5f);

            coin.DOMoveY(coin.position.y - 200, duration * 0.5f).SetEase(rotation);
            coin.DOScale(1, duration * 0.5f).SetEase(rotation);

            yield return new WaitForSecondsRealtime(duration * 0.5f);

            //Affiche le résultat
            coinText.text = headOrTail.coinResultIsHead ? "Head" : "Tail";

            //activer le button
            startTheGame.interactable = true;
            yield return null;
        }
    }
}