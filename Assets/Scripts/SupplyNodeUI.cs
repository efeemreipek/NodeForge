using UnityEngine;

public class SupplyNodeUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer resourceIconSprite;

    private void Awake()
    {
        resourceIconSprite.sprite = GetComponentInParent<SupplyNode>().Resource.Icon;
    }
}
