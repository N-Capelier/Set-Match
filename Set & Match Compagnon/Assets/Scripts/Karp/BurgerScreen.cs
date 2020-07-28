using UnityEngine;
using DG.Tweening;
public class BurgerScreen : MonoBehaviour
{
    [Header("Parametre"), SerializeField]
    float duration = 0.4f;
    [SerializeField] Ease easeType = Ease.InOutCubic;
    protected RectTransform menu;

    private void Awake()
    {
        menu = this.GetComponent<RectTransform>();
    }

    public void BurgerIn()
    {
        menu.DOAnchorPosY(-425, duration, false).SetEase(easeType);
    }

    public void BurgerOut()
    {
        menu.DOAnchorPosY(-700, duration, false).SetEase(easeType);
    }

}
