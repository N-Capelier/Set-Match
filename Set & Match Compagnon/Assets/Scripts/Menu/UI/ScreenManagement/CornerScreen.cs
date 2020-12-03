using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CornerScreen : MonoBehaviour
{
    [Header("Parametre")]
    [SerializeField] float reductSize = 0.4f;
    [SerializeField] float focusSize = 1.25f;
    [SerializeField] Ease easeType = Ease.InOutCubic;
    [SerializeField] Vector2 CenterPos;
    [SerializeField] Vector2 CornerPos;
    [SerializeField] Image fond;
    protected RectTransform menu;

    private void Awake()
    {
        menu = this.GetComponent<RectTransform>();
        desactivation();
    }

    public void CornerViewport(float duration)
    {
        menu.DOAnchorPosX(CornerPos.x, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(CornerPos.y, duration, false).SetEase(easeType);
        menu.DOScale(reductSize, duration).SetEase(easeType);
        fond.DOColor(new Color(1,1,1,0), duration).SetEase(easeType);
        Invoke("desactivation", duration);
    }

    public void CenterViewport(float duration)
    {
        fond.gameObject.SetActive(true);
        menu.DOAnchorPosX(CenterPos.x, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(CenterPos.y, duration, false).SetEase(easeType);
        menu.DOScale(focusSize, duration).SetEase(easeType);
        fond.DOColor(Color.white, duration).SetEase(easeType);
    }

    private void desactivation()
    {
        fond.gameObject.SetActive(false);
    }
}
