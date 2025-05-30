using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class NodeBuildButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonNameText;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void InitializeUI(string buttonName, Action buttonOnClick)
    {
        buttonNameText.text = buttonName;
        button.onClick.AddListener(() => buttonOnClick());
    }
}
