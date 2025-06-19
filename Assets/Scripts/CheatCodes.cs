using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodes : MonoBehaviour
{
    public ResourceList List;

    private void Update()
    {
#if UNITY_EDITOR
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[0], 10);
        }
        if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[1], 10);
        }
        if(Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[2], 10);
        }
        if(Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[3], 10);
        }
        if(Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[4], 10);
        }
        if(Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[5], 10);
        }
        if(Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[6], 10);
        }
        if(Keyboard.current.digit8Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[7], 10);
        }
        if(Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[8], 10);
        }
        if(Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[9], 10);
        }
        if(Keyboard.current.f5Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[10], 10);
        }
        if(Keyboard.current.f6Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[11], 10);
        }
        if(Keyboard.current.f7Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[12], 10);
        }
        if(Keyboard.current.f8Key.wasPressedThisFrame)
        {
            ResourceManager.Instance.AddResource(List.Resources[13], 10);
        }
#endif // if UNITY_EDITOR
    }
}
