using UnityEngine;
using DG.Tweening;

public class UIPanelAnimation : Singleton<UIPanelAnimation>
{
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Ease animationEase = Ease.Linear;

    public void OpenClosePanel(RectTransform panelTransform, bool isVisible, Vector2 onScreenPos, Vector2 offScreenPos)
    {
        panelTransform.DOAnchorPos(isVisible ? onScreenPos : offScreenPos, animationDuration).SetEase(animationEase);
    }
}
