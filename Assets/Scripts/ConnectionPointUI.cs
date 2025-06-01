using UnityEngine;
using TMPro;

public class ConnectionPointUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;

    public void InitializeUI(Resource resource)
    {
        nameText.text = resource.Name;
    }
    public void InitializeUI()
    {
        nameText.text = "ANY";
    }
}
