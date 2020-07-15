using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RegularScreen : MonoBehaviour , IScreen
{
    [SerializeField]
    private RectTransform menu;

    [SerializeField]
    float duration = 0.5f;

    private void OnEnable()
    {
        menu.anchoredPosition = new Vector2(2000, 0);
        EnterViewport();
    }

    public void EnterViewport()
    {
        menu.DOAnchorPosX(0, duration, false);
    }
    public void ExitViewport()
    {
        menu.DOAnchorPosX(2000, duration, false);

        Invoke("disable", duration);
    }

    public IEnumerator ChangeViewport(GameObject screen)
    {
        menu.DOAnchorPosX(2000, duration, false);

        yield return new WaitForSecondsRealtime(duration);

        gameObject.SetActive(false);
        screen.SetActive(true);
    }


    private void disable()
    {
        gameObject.SetActive(false);
    }
}
