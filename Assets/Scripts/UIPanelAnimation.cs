using UnityEngine;
using DG.Tweening;

public class UIPanelAnimation : MonoBehaviour
{
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Ease animationEase = Ease.Linear;
    [SerializeField] private RectTransform minePanel;
    [SerializeField] private RectTransform buildPanel;

    private Vector2 minePanelOffScreenPos = new Vector2(300, 0);
    private Vector2 minePanelOnScreenPos = new Vector2(0, 0);
    private bool isMinePanelVisible = true;

    private Vector2 buildPanelOffScreenPos = new Vector2(-300, 0);
    private Vector2 buildPanelOnScreenPos = new Vector2(0, 0);
    private bool isBuildPanelVisible = true;

    public void OpenCloseMinePanel()
    {
        minePanel.DOAnchorPos(isMinePanelVisible ? minePanelOffScreenPos : minePanelOnScreenPos, animationDuration)
            .SetEase(animationEase);

        isMinePanelVisible = !isMinePanelVisible;
    }
    public void OpenCloseBuildPanel()
    {
        buildPanel.DOAnchorPos(isBuildPanelVisible ? buildPanelOffScreenPos : buildPanelOnScreenPos, animationDuration)
            .SetEase(animationEase);

        isBuildPanelVisible = !isBuildPanelVisible;
    }
}
