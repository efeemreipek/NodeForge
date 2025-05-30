using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputHandler : Singleton<InputHandler>
{
    [SerializeField] private InputActionAsset inputActionAsset;

    private InputActionMap actionMapGame;
    private InputActionMap actionMapCamera;

    private InputAction actionMousePosition;
    private InputAction actionMouseLeftClick;
    private InputAction actionCameraMove;
    private InputAction actionCameraZoom;
    private InputAction actionCameraDrag;

    private Vector2 mousePosition;
    private bool mouseLeftClickPressed, mouseLeftClickHold, mouseLeftClickReleased;
    private Vector2 cameraMove;
    private float cameraZoom;
    private bool cameraDragPressed, cameraDragHold, cameraDragReleased;

    public Vector2 MousePosition => mousePosition;
    public bool MouseLeftClickPressed => mouseLeftClickPressed;
    public bool MouseLeftClickHold => mouseLeftClickHold;
    public bool MouseLeftClickReleased => mouseLeftClickReleased;
    public Vector2 CameraMove => cameraMove;
    public float CameraZoom => cameraZoom;
    public bool CameraDragPressed => cameraDragPressed;
    public bool CameraDragHold => cameraDragHold;
    public bool CameraDragReleased => cameraDragReleased;

    private void OnEnable()
    {
        if(inputActionAsset == null) inputActionAsset = InputSystem.actions;

        InitializeActionMaps();

        actionMapGame.Enable();
        actionMapCamera.Enable();

        InitializeInputActions();
        SubscribeToInputEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromInputEvents();

        actionMapGame.Disable();
        actionMapCamera.Disable();
    }
    private void Update()
    {
        if(IsMouseOverUI())
        {
            actionCameraZoom.Disable();
            actionCameraDrag.Disable();
        }
        else
        {
            actionCameraZoom.Enable();
            actionCameraDrag.Enable();
        }
    }
    private void LateUpdate()
    {
        mouseLeftClickPressed = false;
        mouseLeftClickReleased = false;

        cameraDragPressed = false;
        cameraDragReleased = false;
    }

    private bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();
    private void InitializeActionMaps()
    {
        actionMapGame = inputActionAsset.FindActionMap("Game");
        actionMapCamera = inputActionAsset.FindActionMap("Camera");
    }
    private void InitializeInputActions()
    {
        actionMousePosition = actionMapGame.FindAction("Mouse Position");
        actionMouseLeftClick = actionMapGame.FindAction("Mouse Left Click");

        actionCameraMove = actionMapCamera.FindAction("Camera Move");
        actionCameraZoom = actionMapCamera.FindAction("Camera Zoom");
        actionCameraDrag = actionMapCamera.FindAction("Camera Drag");
    }
    private void SubscribeToInputEvents()
    {
        actionMousePosition.performed += MousePosition_Performed;
        actionMousePosition.canceled += MousePosition_Canceled;

        actionMouseLeftClick.performed += MouseLeftClick_Performed;
        actionMouseLeftClick.canceled += MouseLeftClick_Canceled;

        actionCameraMove.performed += CameraMove_Performed;
        actionCameraMove.canceled += CameraMove_Canceled;

        actionCameraZoom.performed += CameraZoom_Performed;
        actionCameraZoom.canceled += CameraZoom_Canceled;

        actionCameraDrag.performed += CameraDrag_Performed;
        actionCameraDrag.canceled += CameraDrag_Canceled;
    }
    private void UnsubscribeFromInputEvents()
    {
        actionMousePosition.performed -= MousePosition_Performed;
        actionMousePosition.canceled -= MousePosition_Canceled;

        actionMouseLeftClick.performed -= MouseLeftClick_Performed;
        actionMouseLeftClick.canceled -= MouseLeftClick_Canceled;

        actionCameraMove.performed -= CameraMove_Performed;
        actionCameraMove.canceled -= CameraMove_Canceled;

        actionCameraZoom.performed -= CameraZoom_Performed;
        actionCameraZoom.canceled -= CameraZoom_Canceled;

        actionCameraDrag.performed -= CameraDrag_Performed;
        actionCameraDrag.canceled -= CameraDrag_Canceled;
    }

    private void MousePosition_Performed(InputAction.CallbackContext obj) => mousePosition = obj.ReadValue<Vector2>();
    private void MousePosition_Canceled(InputAction.CallbackContext obj) => mousePosition = obj.ReadValue<Vector2>();
    private void MouseLeftClick_Performed(InputAction.CallbackContext obj)
    {
        mouseLeftClickPressed = true;
        mouseLeftClickHold = true;
    }
    private void MouseLeftClick_Canceled(InputAction.CallbackContext obj)
    {
        mouseLeftClickHold = false;
        mouseLeftClickReleased = true;
    }
    private void CameraMove_Performed(InputAction.CallbackContext obj) => cameraMove = obj.ReadValue<Vector2>();
    private void CameraMove_Canceled(InputAction.CallbackContext obj) => cameraMove = Vector2.zero;
    private void CameraZoom_Performed(InputAction.CallbackContext obj) => cameraZoom = obj.ReadValue<float>();
    private void CameraZoom_Canceled(InputAction.CallbackContext obj) => cameraZoom = 0f;
    private void CameraDrag_Performed(InputAction.CallbackContext obj)
    {
        cameraDragPressed = true;
        cameraDragHold = true;
    }
    private void CameraDrag_Canceled(InputAction.CallbackContext obj)
    {
        cameraDragHold = false;
        cameraDragReleased = true;
    }
}
