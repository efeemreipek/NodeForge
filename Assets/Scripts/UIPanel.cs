using UnityEngine;
using TMPro;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private bool isVisible;
    [SerializeField] private Vector2 onScreenPosition;
    [SerializeField] private Vector2 offScreenPosition;
    [SerializeField] private TMP_Text openCloseButtonText;
    [SerializeField] private char openChar;
    [SerializeField] private char closeChar;

    private void Awake()
    {
        InitializePanel();
    }

    private void InitializePanel()
    {
        openCloseButtonText.text = isVisible ? closeChar.ToString() : openChar.ToString();
        UIPanelAnimation.Instance.OpenClosePanel((RectTransform)transform, isVisible, onScreenPosition, offScreenPosition);
    }
    public void ConvertVisibility()
    {
        isVisible = !isVisible;

        openCloseButtonText.text = isVisible ? closeChar.ToString() : openChar.ToString();
        UIPanelAnimation.Instance.OpenClosePanel((RectTransform)transform, isVisible, onScreenPosition, offScreenPosition);
    }
}
