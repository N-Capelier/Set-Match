using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField]
    private RectTransform menu;

    [SerializeField]
    float duration = 0.5f;

    private void OnEnable()
    {
        CenterViewport();
    }

    public void CenterViewport()
    {
        menu.DOAnchorPosX(0, duration, false);
        menu.DOAnchorPosY(0, duration, false);
    }

    public void EnterViewport()
    {
        menu.anchoredPosition = new Vector2(2000, 0);
        menu.DOAnchorPosX(0, duration, false);
    }

    public void ViewportComeback()
    {
        menu.anchoredPosition = new Vector2(-2000, 0);
        menu.DOAnchorPosX(0, duration, false);
    }

    public void ExitViewport()
    {
        menu.DOAnchorPosX(2000, duration, false);

        Invoke("disable", duration);
    }

    private void disable()
    {
        gameObject.SetActive(false);
    }
}
