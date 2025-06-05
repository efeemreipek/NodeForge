using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TMP_Text resourceAmountText;

    private Resource resource;

    private void OnEnable()
    {
        ResourceManager.Instance.OnResourceAmountChanged += UpdateUI;
    }
    private void OnDisable()
    {
        if(ResourceManager.HasInstance) ResourceManager.Instance.OnResourceAmountChanged -= UpdateUI;
    }

    public void InitializeUI(Resource resource, Sprite icon, int amount)
    {
        this.resource = resource;
        resourceIcon.sprite = icon;
        resourceAmountText.text = amount.ToString();
    }
    private void UpdateUI(Resource resource, int amount)
    {
        if(this.resource != resource) return;

        resourceAmountText.text = amount.ToString();
    }
}
