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
        ResourceManager.Instance.OnResourceAmountChanged -= UpdateUI;
    }

    public void InitializeUI(Resource resource, Sprite icon, float amount)
    {
        this.resource = resource;
        resourceIcon.sprite = icon;
        resourceAmountText.text = amount.ToString();
    }
    private void UpdateUI(Resource resource, float amount)
    {
        if(this.resource != resource) return;

        resourceAmountText.text = amount.ToString();
    }
}
