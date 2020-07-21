using UnityEngine;
using DG.Tweening;

/// <summary>
/// ARD
/// </summary>
public class BurgerScreen : MonoBehaviour
{
    [Header("Parametre"), SerializeField]
    float duration = 0.4f;
    [SerializeField] Ease easeType = Ease.InOutCubic;
    protected RectTransform menu;
    [SerializeField] private float inHeight = -425, outHeight = -700;

    private void Awake()
    {
        menu = this.GetComponent<RectTransform>();
    }

    public void BurgerIn()
    {
        menu.DOAnchorPosY(inHeight, duration, false).SetEase(easeType);
    }

    public void BurgerOut()
    {
        menu.DOAnchorPosY(outHeight, duration, false).SetEase(easeType);
    }

}
