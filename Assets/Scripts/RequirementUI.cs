using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequirementUI : MonoBehaviour
{
    [SerializeField] private Image requirementImage;
    [SerializeField] private TMP_Text requirementText;

    public void InitializeRequirement(ResourceAmount ra)
    {
        requirementImage.sprite = ra.Resource.Icon;
        requirementText.text = ra.Amount.ToString();
    }
}
