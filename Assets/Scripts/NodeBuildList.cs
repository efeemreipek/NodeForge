using UnityEngine;

public class NodeBuildList : MonoBehaviour
{
    [SerializeField] private RectTransform scrollViewContentTransform;
    [SerializeField] private GameObject nodeBuildButtonPrefab;
    [SerializeField] private NodeDataList nodeDataList;

    private void Start()
    {
        for(int i = 0; i < nodeDataList.Nodes.Count; i++)
        {
            GameObject buttonGO = Instantiate(nodeBuildButtonPrefab, scrollViewContentTransform);
            NodeBuildButton nodeBuildButton = buttonGO.GetComponent<NodeBuildButton>();
            nodeBuildButton.InitializeButton(nodeDataList.Nodes[i].Name, nodeDataList.Nodes[i].Prefab);
            nodeBuildButton.InitializeRequirements(nodeDataList.Nodes[i].HasBuildRequirements, nodeDataList.Nodes[i].BuildRequirements);
        }
    }
}
