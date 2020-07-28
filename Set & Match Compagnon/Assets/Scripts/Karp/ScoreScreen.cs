using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [Header("Parametre"), SerializeField]
    float duration = 0.8f;
    [SerializeField] Ease easeType = Ease.InOutCubic;
    [SerializeField] Vector2 CornerPos;
    [SerializeField] Color transpColor, opaqueColor;
    [SerializeField] Image fond;
    protected RectTransform menu;

    private void Awake()
    {
        menu = this.GetComponent<RectTransform>();
    }

    public void CornerViewport()
    {
        menu.DOAnchorPosX(CornerPos.x, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(CornerPos.y, duration, false).SetEase(easeType);
        menu.DOScale(0.3f, duration).SetEase(easeType);
        fond.DOColor(transpColor, duration).SetEase(easeType);

    }

    public void CenterViewport()
    {
        menu.DOAnchorPosX(0, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(0, duration, false).SetEase(easeType);
        menu.DOScale(1, duration).SetEase(easeType);
        fond.DOColor(opaqueColor, duration).SetEase(easeType);

    }
}
