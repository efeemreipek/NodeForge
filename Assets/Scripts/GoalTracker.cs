using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTracker : MonoBehaviour
{
    [SerializeField] private Resource goalResource;
    [SerializeField] private int goalAmount = 100;
    [SerializeField] private TMP_Text goalAmountText;

    private void OnEnable()
    {
        ResourceManager.Instance.OnResourceAmountChanged += OnResourceAmountChanged;
    }
    private void OnDisable()
    {
        if(ResourceManager.HasInstance) ResourceManager.Instance.OnResourceAmountChanged += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged(Resource resource, int amount)
    {
        if(resource != goalResource) return;
        goalAmountText.text = $"({amount}/{goalAmount})";
        
        if(amount >= goalAmount)
        {
            StartCoroutine(EndGame());
        }
    }
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
