using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class HeadOrTail_Visual : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private HeadOrTail headOrTail;
        [Space(10)]
        [SerializeField] private RectTransform coin;
        [SerializeField] private TextMeshProUGUI coinText;

        [Header("Variable")]
        [SerializeField] private float launchDuration = 2f;
        [SerializeField] private Ease rotation = Ease.InOutCubic;
        [SerializeField, Range(1,24)] private int flipNumber = 6;

        private void OnEnable()
        {
            headOrTail.onCoinLauch += OnCoinLauch;
        }
        private void OnDisable()
        {
            headOrTail.onCoinLauch -= OnCoinLauch;
        }

        public void OnCoinLauch()
        {
            StopAllCoroutines();
            StartCoroutine(CoinFlipping(launchDuration, flipNumber));
        }

        IEnumerator CoinFlipping(float duration,int flipIteration)
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
            coinText.text = headOrTail.coinResult_Head ? "Head" : "Tail";

            yield return null;
        }
    }
}
