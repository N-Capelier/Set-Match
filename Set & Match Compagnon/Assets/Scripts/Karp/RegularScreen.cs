using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RegularScreen : MonoBehaviour , IScreen
{
    [Header("Parametre") , SerializeField]
    float duration = 0.5f;
    [SerializeField] Ease easeType = Ease.InElastic;
    private RectTransform menu;

    private void Awake()
    {
        menu = this.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        CenterViewport();
    }

    public void EnterViewportH(float posX)
    {
        menu.anchoredPosition = new Vector2(posX, 0);
        menu.DOAnchorPosX(0, duration, false).SetEase(easeType);
    }
    public void ExitViewportH(float posX)
    {
        menu.DOAnchorPosX(posX, duration, false).SetEase(easeType);
        Invoke("disable", duration);
    }
    public void EnterViewportV(float posY)
    {
        menu.anchoredPosition = new Vector2(0, posY);
        menu.DOAnchorPosY(0, duration, false).SetEase(easeType);
    }
    public void ExitViewportV(float posY)
    {
        menu.DOAnchorPosY(posY, duration, false).SetEase(easeType);
        Invoke("disable", duration);
    }

    public void CenterViewport()
    {
        menu.DOAnchorPosX(0, duration, false).SetEase(easeType);
        menu.DOAnchorPosY(0, duration, false).SetEase(easeType);
    }
    
    public void SetActiveScreenFromH(float posX)
    {
        gameObject.SetActive(true);
        menu.anchoredPosition = new Vector2(posX, 0);
    }
    public void SetActiveScreenFromV(float posY)
    {
        gameObject.SetActive(true);
        menu.anchoredPosition = new Vector2(0, posY);
    }


    private void disable()
    {
        gameObject.SetActive(false);
    }
}
