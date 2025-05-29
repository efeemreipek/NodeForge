using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public string RecipeName;
    public float ProductionTime = 1f;
    public List<ResourceAmount> Inputs = new List<ResourceAmount>();
    public List<ResourceAmount> Outputs = new List<ResourceAmount>();
}
