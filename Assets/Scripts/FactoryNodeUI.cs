using UnityEngine;

public class FactoryNodeUI : MonoBehaviour
{
    [SerializeField] private Transform progressBar;

    public void UpdateProgressBar(float progress)
    {
        progressBar.localScale = new Vector3(progress, 1f, 1f);
    }
}
