using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private Vector2 zoomedInBounds;
    [SerializeField] private Vector2 zoomedOutBounds;

    private Camera cam;
    private bool isDragging;
    private Vector2 lastMousePosition;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        HandleMovement();
        HandleDrag();
        HandleZoom();
        ClampPosition();
    }

    private void HandleDrag()
    {
        if(InputHandler.Instance.CameraDragPressed)
        {
            isDragging = true;
            lastMousePosition = InputHandler.Instance.MousePosition;
        }
        if(InputHandler.Instance.CameraDragHold && isDragging)
        {
            Vector2 delta = InputHandler.Instance.MousePosition - lastMousePosition;
            Vector2 move = -delta * dragSpeed * cam.orthographicSize;
            transform.position += new Vector3(move.x, move.y);
            lastMousePosition = InputHandler.Instance.MousePosition;
        }
        if(InputHandler.Instance.CameraDragReleased)
        {
            isDragging = false;
        }
    }
    private void HandleMovement()
    {
        Vector2 move = InputHandler.Instance.CameraMove * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(move.x, move.y);
    }
    private void HandleZoom()
    {
        float zoomInput = InputHandler.Instance.CameraZoom;
        if(zoomInput != 0f)
        {
            float newSize = cam.orthographicSize - InputHandler.Instance.CameraZoom * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
    private void ClampPosition()
    {
        float t = Mathf.InverseLerp(minZoom, maxZoom, cam.orthographicSize);

        float dynamicXBound = Mathf.Lerp(zoomedInBounds.x, zoomedOutBounds.x, t);
        float dynamicYBound = Mathf.Lerp(zoomedInBounds.y, zoomedOutBounds.y, t);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -dynamicXBound, dynamicXBound);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -dynamicYBound, dynamicYBound);

        transform.position = clampedPosition;
    }
}
