using UnityEngine;
using UnityEngine.UI;

public class FactoryNodeUI : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;

    public void UpdateProgressBar(float progress)
    {
        progressBarImage.fillAmount = progress;
    }
}
