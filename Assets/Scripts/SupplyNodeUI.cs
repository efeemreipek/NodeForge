using UnityEngine;

public class SupplyNodeUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer resourceIconSprite;
    [SerializeField] private Transform progressBar;

    private void Awake()
    {
        resourceIconSprite.sprite = GetComponentInParent<SupplyNode>().Resource.Icon;
    }

    public void UpdateProgressBar(float progress)
    {
        progressBar.localScale = new Vector3(progress, 1f, 1f);
    }
}
