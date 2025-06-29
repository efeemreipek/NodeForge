using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeBuildButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonNameText;
    [SerializeField] private GameObject requirementPrefab;
    [SerializeField] private RectTransform requirementsContainer;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void InitializeUI(string buttonName, Action buttonOnClick)
    {
        buttonNameText.text = buttonName;
        button.onClick.AddListener(() => buttonOnClick());
        button.onClick.AddListener(AudioManager.Instance.PlayBuildClick);
    }
    public void SetButtonEnabled(bool enabled)
    {
        button.interactable = enabled;
    }

    internal void SetupRequirements(List<ResourceAmount> buildRequirements)
    {
        foreach(ResourceAmount ra in buildRequirements)
        {
            GameObject reqGO = Instantiate(requirementPrefab, requirementsContainer);
            RequirementUI req = reqGO.GetComponent<RequirementUI>(); 
            req.InitializeRequirement(ra);
        }
    }
}
