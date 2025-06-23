using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class NodeController : Singleton<NodeController>
{
    [SerializeField] private LayerMask nodeLayer;
    [SerializeField] private float nodeMoveDuration = 0.1f;
    [SerializeField] private Ease nodeMoveEase = Ease.OutQuad;
    [SerializeField] private Canvas selectionCanvas;
    [SerializeField] private RectTransform selectionBoxRect;

    private List<Node> selectedNodes = new List<Node>();
    private Vector3 lastMousePosition;
    private Dictionary<Node, Vector3> nodeOffsets = new Dictionary<Node, Vector3>();
    private bool isDragging;
    private bool isSelecting;
    private Vector2 selectionStartPos;
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

        if(InputHandler.Instance.MouseLeftClickPressed)
        {
            Collider2D hit = Physics2D.OverlapPoint(mouseWorld, nodeLayer);
            Collider2D connectionHit = Physics2D.OverlapPoint(mouseWorld, LayerMask.GetMask("Connection Point"));
            if(hit == null && connectionHit == null)
            {
                StartSelectionBox();
            }
            else
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
        }
        if(isSelecting)
        {
            UpdateSelectionBox();
        }

        if(InputHandler.Instance.MouseLeftClickHold && selectedNodes.Count > 0 && !isSelecting)
        {
            if(!isDragging)
            {
                isDragging = true;
                CalculateNodeOffsets(mouseWorld);
            }
            MoveSelectedNodes(mouseWorld);
        }
        if(InputHandler.Instance.MouseLeftClickReleased)
        {
            if(isSelecting)
            {
                EndSelectionBox();
            }
            isDragging = false;
            nodeOffsets.Clear();
        }
        if(InputHandler.Instance.MouseRightClickPressed)
        {
            TryDeleteNodes(mouseWorld);
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
                AddNodeToSelected(node);
            }
        }
        else
        {
            ClearSelection();
            ClearNodesFromSelected();
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
                    RemoveNodeFromSelected(node);
                }
                else
                {
                    AddNodeToSelected(node);
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

            ClearNodesFromSelected();
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
    private void StartSelectionBox()
    {
        if(selectionBoxRect == null) return;

        isSelecting = true;
        selectionStartPos = InputHandler.Instance.MousePosition;
        selectionBoxRect.gameObject.SetActive(true);

        if(!InputHandler.Instance.ShiftHold)
        {
            ClearSelection();
        }

        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            selectionCanvas.transform as RectTransform,
            selectionStartPos,
            selectionCanvas.worldCamera,
            out canvasPos
            );

        selectionBoxRect.anchoredPosition = canvasPos;
        selectionBoxRect.sizeDelta = Vector2.zero;
    }
    private void UpdateSelectionBox()
    {
        if(selectionBoxRect ==  null && !isSelecting) return;

        Vector2 currentMousePos = InputHandler.Instance.MousePosition;

        Vector2 startCanvasPos, currentCanvasPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            selectionCanvas.transform as RectTransform,
            selectionStartPos,
            selectionCanvas.worldCamera,
            out startCanvasPos
        );

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            selectionCanvas.transform as RectTransform,
            currentMousePos,
            selectionCanvas.worldCamera,
            out currentCanvasPos
        );

        Vector2 lowerLeft = new Vector2(Mathf.Min(startCanvasPos.x, currentCanvasPos.x), Mathf.Min(startCanvasPos.y, currentCanvasPos.y));
        Vector2 upperRight = new Vector2(Mathf.Max(startCanvasPos.x, currentCanvasPos.x), Mathf.Max(startCanvasPos.y, currentCanvasPos.y));

        selectionBoxRect.anchoredPosition = lowerLeft;
        selectionBoxRect.sizeDelta = upperRight - lowerLeft;
    }
    private void EndSelectionBox()
    {
        if(selectionBoxRect == null && !isSelecting) return;

        isSelecting = false;
        selectionBoxRect.gameObject.SetActive(false);

        Vector2 startScreenPos = selectionStartPos;
        Vector2 currentScreenPos = InputHandler.Instance.MousePosition;

        Vector2 lowerLeftScreen = new Vector2(Mathf.Min(startScreenPos.x, currentScreenPos.x), Mathf.Min(startScreenPos.y, currentScreenPos.y));
        Vector2 upperRightScreen = new Vector2(Mathf.Max(startScreenPos.x, currentScreenPos.x), Mathf.Max(startScreenPos.y, currentScreenPos.y));

        Vector3 worldLowerLeft = cam.ScreenToWorldPoint(new Vector3(lowerLeftScreen.x, lowerLeftScreen.y, cam.nearClipPlane));
        Vector3 worldUpperRight = cam.ScreenToWorldPoint(new Vector3(upperRightScreen.x, upperRightScreen.y, cam.nearClipPlane));

        worldLowerLeft.z = 0f;
        worldUpperRight.z = 0f;

        Collider2D[] nodesInArea = Physics2D.OverlapAreaAll(new Vector2(worldLowerLeft.x, worldLowerLeft.y), new Vector2(worldUpperRight.x, worldUpperRight.y), nodeLayer);

        List<Node> newSelectedNodes = new List<Node>();

        foreach(Collider2D nodeCollider in nodesInArea)
        {
            Node node = nodeCollider.GetComponent<Node>();
            if(node != null)
            {
                if(!newSelectedNodes.Contains(node))
                {
                    newSelectedNodes.Add(node);
                }
            }
        }

        foreach(Node node in newSelectedNodes)
        {
            if(node != null)
            {
                if(selectedNodes.Contains(node))
                {
                    RemoveNodeFromSelected(node);
                }
                else
                {
                    AddNodeToSelected(node);
                }
            }
        }
    }
    private void AddNodeToSelected(Node node)
    {
        selectedNodes.Add(node);
        node.SelectedVisualGO.SetActive(true);
        node.BringToFront();
    }
    private void RemoveNodeFromSelected(Node node)
    {
        selectedNodes.Remove(node);
        node.SelectedVisualGO.SetActive(false);
    }
    private void ClearNodesFromSelected()
    {
        foreach(Node node in selectedNodes)
        {
            node.SelectedVisualGO.SetActive(false);
        }
        selectedNodes.Clear();
    }
    public void SelectSingleNode(Node node)
    {
        ClearSelection();
        AddNodeToSelected(node);
    }
    private void ClearSelection()
    {
        ClearNodesFromSelected();
        nodeOffsets.Clear();
        isDragging = false; 
    }
    public Vector3 SnapToGrid(Vector3 position)
    {
        float cellSize = GridDrawer.Instance.CellSize;
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        return new Vector3(x, y, 0f);
    }
}
