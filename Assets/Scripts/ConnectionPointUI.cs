using UnityEngine;
using TMPro;

public class ConnectionPointUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer circleSprite;
    [SerializeField] private TMP_Text nameText;

    public void InitializeUI(Resource resource)
    {
        circleSprite.color = resource.Color;
        nameText.text = resource.Name;
    }
    public void InitializeUI()
    {
        nameText.text = "ANY";
    }
}
