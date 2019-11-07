using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    public enum InteractionType
    {
        Default,
        Close,
        Lock,
        Open,
        Put,
        TurnOff,
        TurnOn,
        Unlock
    }

    [SerializeField]
    private InteractionType interactionType = default;

    [SerializeField]
    private string interactionText = null;

    [SerializeField]
    private Item needsItem = null;

    [SerializeField]
    private Item giveItem = null;

    [SerializeField]
    private bool removeNeededItem = false;

    [SerializeField]
    private Interaction replaceInteraction = null;

    [SerializeField]
    private string replaceTargetByName = null;

    [SerializeField]
    private bool newInteractionNeedsForce;

    [SerializeField]
    private bool removeTargetFromWorld;

    // Description is used to describe the interaction in the Unity-editor, if needed. It is not used anywhere else.
    [TextArea(15, 15)]
    public string Description;

    public string InteractionText { get => interactionText; set => interactionText = value; }
    public Item NeedItem { get => needsItem; set => needsItem = value; }
    public bool RemoveNeededItem { get => removeNeededItem; set => removeNeededItem = value; }
    public Item GiveItem { get => giveItem; set => giveItem = value; }
    public bool RemoveTargetFromWorld { get => removeTargetFromWorld; set => removeTargetFromWorld = value; }
    public Interaction ReplaceInteraction { get => replaceInteraction; set => replaceInteraction = value; }
    public string ReplaceTargetByName { get => replaceTargetByName; set => replaceTargetByName = value; }
    public bool NewInteractioNeedsForce { get => newInteractionNeedsForce; set => newInteractionNeedsForce = value; }

    public virtual void Interact(InteractionTarget target)
    {
        target.Interact(interactionType);
    }
}