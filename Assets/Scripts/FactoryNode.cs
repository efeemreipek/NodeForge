using UnityEngine;

public class FactoryNode : Node
{
    public Recipe Recipe;

    private void Awake()
    {
        for(int i = 0; i < InputPoints.Count; i++)
        {
            InputPoints[i].InitializeConnectionPoint(Recipe.Inputs[i].Resource);
        }
        for(int i = 0; i < OutputPoints.Count; i++)
        {
            OutputPoints[i].InitializeConnectionPoint(Recipe.Outputs[i].Resource);
        }
    }
}
