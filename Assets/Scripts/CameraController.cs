using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Vector3 move = InputHandler.Instance.CameraMove * moveSpeed * Time.deltaTime;
        transform.position += move;

        if(InputHandler.Instance.CameraZoom != 0f)
        {
            float newSize = cam.orthographicSize - InputHandler.Instance.CameraZoom * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}
