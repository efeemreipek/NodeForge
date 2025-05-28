using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Scriptable Objects/Resource")]
public class Resource : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Color Color;
}
