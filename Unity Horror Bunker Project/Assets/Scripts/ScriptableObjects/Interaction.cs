using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    public string InteractionMethod = "Default";

    public string InteractionValue = null;

    public Item NeedItem = null;

    public Item GiveItem = null;

    public Note GiveNote = null;

    public bool RemoveNeededItem = false;

    public Interaction ReplaceInteraction = null;

    public GameObject ReplaceTarget = null;

    public bool NewInteractionNeedsForce;

    public bool RemoveTargetFromWorld;

    // Description is used to describe the interaction in the Unity-editor, if needed. It is not used anywhere else.
    [TextArea(15, 15)]
    public string Description;
}