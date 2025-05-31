using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource List", menuName = "Scriptable Objects/Resource List")]
public class ResourceList : ScriptableObject
{
    public List<Resource> Resources = new List<Resource>();
}
