using UnityEngine;
using UnityEngine.UI;

public class SupplyNodeUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer resourceIconSprite;
    [SerializeField] private Image progressBarImage;

    private void Awake()
    {
        resourceIconSprite.sprite = GetComponentInParent<SupplyNode>().Resource.Icon;
    }

    public void UpdateProgressBar(float progress)
    {
        progressBarImage.fillAmount = progress;
    }
}
