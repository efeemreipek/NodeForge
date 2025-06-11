using UnityEngine;
using UnityEngine.UI;

public class GeneratorNodeUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer resourceIconSprite;
    [SerializeField] private Image progressBarImage;

    private void Awake()
    {
        resourceIconSprite.sprite = GetComponentInParent<GeneratorNode>().Resource.Icon;
    }

    public void UpdateProgressBar(float progress)
    {
        progressBarImage.fillAmount = progress;
    }
}
