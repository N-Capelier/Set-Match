using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [Header("Parametre"), SerializeField]
    float duration = 0.8f;
    [Space(10)]
    [SerializeField] float reductSize = 0.4f;
    [SerializeField] float focusSize = 1.25f;
    [SerializeField] Ease easeType = Ease.InOutCubic;
    [SerializeField] Vector2 CornerPos;
    [SerializeField] Color transpColor, opaqueColor;
    [SerializeField] Image fond;
    protected RectTransform menu;

    private void Awake()
    {
        menu = this.GetComponent<RectTransform>();
        desact();
    }

    public void CornerViewport()
    {
        menu.DOAnchorPosX(CornerPos.x, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(CornerPos.y, duration, false).SetEase(easeType);
        menu.DOScale(reductSize, duration).SetEase(easeType);
        fond.DOColor(transpColor, duration).SetEase(easeType);
        Invoke("desact", duration);
    }

    public void CenterViewport()
    {
        fond.gameObject.SetActive(true);
        menu.DOAnchorPosX(0, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(0, duration, false).SetEase(easeType);
        menu.DOScale(focusSize, duration).SetEase(easeType);
        fond.DOColor(opaqueColor, duration).SetEase(easeType);
    }

    private void desact()
    {
        fond.gameObject.SetActive(false);
    }
}
