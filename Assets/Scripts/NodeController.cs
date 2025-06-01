using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class NodeController : Singleton<NodeController>
{
    [SerializeField] private LayerMask nodeLayer;
    [SerializeField] private float nodeMoveDuration = 0.1f;
    [SerializeField] private Ease nodeMoveEase = Ease.OutQuad;

    private List<Node> selectedNodes = new List<Node>();
    private Vector3 lastMousePosition;
    private Dictionary<Node, Vector3> nodeOffsets = new Dictionary<Node, Vector3>();
    private bool isDragging;
    private Camera cam;

    protected override void Awake()
    {
        base.Awake();

        cam = Camera.main;
    }
    private void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(InputHandler.Instance.MousePosition);
        mouseWorld.z = 0f;

        if(InputHandler.Instance.MouseLeftClickHold && selectedNodes.Count > 0)
        {
            if(!isDragging)
            {
                isDragging = true;
                CalculateNodeOffsets(mouseWorld);
            }
            MoveSelectedNodes(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickPressed)
        {
            if(InputHandler.Instance.ShiftHold)
            {
                TryToggleNodeSelection(mouseWorld);
            }
            else
            {
                TrySelectSingleNode(mouseWorld);
            }
        }
        if(InputHandler.Instance.MouseRightClickPressed)
        {
            TryDeleteNodes(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickReleased)
        {
            isDragging = false;
            nodeOffsets.Clear();
        }
    }

    private void TrySelectSingleNode(Vector3 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, nodeLayer);

        if(hit != null)
        {
            Node node = hit.GetComponent<Node>();
            if(node != null)
            {
                if(selectedNodes.Contains(node)) return;

                ClearSelection();
                selectedNodes.Add(node);
            }
        }
        else
        {
            ClearSelection(); selectedNodes.Clear();
        }
    }
    private void TryToggleNodeSelection(Vector3 mouseWorld)
    {
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, nodeLayer);
        if(hit != null)
        {
            Node node = hit.GetComponent<Node>();
            if(node != null)
            {
                if(selectedNodes.Contains(node))
                {
                    selectedNodes.Remove(node);
                }
                else
                {
                    selectedNodes.Add(node);
                }

                if(selectedNodes.Count > 0)
                {
                    CalculateNodeOffsets(mouseWorld);
                }
            }
        }
    }
    private void TryDeleteNodes(Vector3 mouseWorld)
    {
        if(selectedNodes.Count > 0)
        {
            foreach(Node node in selectedNodes)
            {
                if(node != null)
                {
                    Destroy(node.gameObject);
                }
            }

            selectedNodes.Clear();
            nodeOffsets.Clear();
        }
        else
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorld, nodeLayer);
            if(hit != null)
            {
                Node node = hit.GetComponent<Node>();
                if(node != null)
                {
                    Destroy(node.gameObject);
                }
            }
        }
    }
    private void MoveSelectedNodes(Vector3 mouseWorld)
    {
        foreach(Node node in selectedNodes)
        {
            if(node != null && nodeOffsets.ContainsKey(node))
            {
                Vector3 targetPosition = SnapToGrid(mouseWorld + nodeOffsets[node]);

                if(Vector3.Distance(node.transform.position, targetPosition) > 0.1f)
                {
                    node.transform.DOMove(targetPosition, nodeMoveDuration).SetEase(nodeMoveEase);
                }
            }
        }
    }
    private void CalculateNodeOffsets(Vector3 mousePosition)
    {
        nodeOffsets.Clear();
        foreach(Node node in selectedNodes)
        {
            if(node != null)
            {
                nodeOffsets[node] = node.transform.position - mousePosition;
            }
        }
    }
    private void ClearSelection()
    {
        selectedNodes.Clear();
        nodeOffsets.Clear();
        isDragging = false; 
    }
    private Vector3 SnapToGrid(Vector3 position)
    {
        float cellSize = GridDrawer.Instance.CellSize;
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        return new Vector3(x, y, 0f);
    }
}
