using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;

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
}
