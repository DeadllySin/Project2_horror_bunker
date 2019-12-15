using UnityEngine;

public abstract class InteractableItem : ScriptableObject
{

    [TextArea(5, 15)]
    public string Description;
}