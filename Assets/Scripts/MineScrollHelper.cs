using UnityEngine;
using UnityEngine.UI;

public class MineScrollHelper : MonoBehaviour
{
    [SerializeField] private Transform contentTransform;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float snapSpeed = 5f;
    [SerializeField] private float snapThreshold = 0.001f;

    private int childCount;
    private float[] snapPositions;
    private float targetPosition;
    private bool isSnapping;

    private void Awake()
    {
        childCount = contentTransform.childCount;
        scrollbar.size = (1f / childCount);

        if(childCount > 1)
        {
            scrollbar.size = 1f / childCount;

            snapPositions = new float[childCount];
            for(int i = 0; i < childCount; i++)
            {
                snapPositions[i] = (float)i / (childCount - 1);
            }
        }
        else
        {
            snapPositions = new float[] { 0f };
        }

        scrollbar.value = 0f;
    }
    private void Update()
    {
        if(isSnapping)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPosition, snapSpeed * Time.deltaTime);

            if(Mathf.Abs(scrollbar.value - targetPosition) < snapThreshold)
            {
                scrollbar.value = targetPosition;
                isSnapping = false;
            }
        }
    }

    private float FindClosestSnap(float current)
    {
        float closest = snapPositions[0];
        float minDistance = Mathf.Abs(current - closest);

        for(int i = 1; i < snapPositions.Length; i++)
        {
            float distance = Mathf.Abs(current - snapPositions[i]);
            if(distance < minDistance)
            {
                closest = snapPositions[i];
                minDistance = distance;
            }
        }

        return closest;
    }
    public void ScrollValueChanged(Vector2 value)
    {
        if(isSnapping) return;

        float currentScroll = value.x;
        targetPosition = FindClosestSnap(currentScroll);
        isSnapping = true;
    }
}
