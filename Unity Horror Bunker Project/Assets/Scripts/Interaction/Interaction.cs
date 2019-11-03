using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    public enum InteractionType
    {
        Default,
        Open,
        Close,
        Lock,
        Unlock,
        Take,
        Put
    }

    [SerializeField]
    private InteractionType interactionType = default;

    [SerializeField]
    private string interactionText = null;

    [SerializeField]
    private Item needsItem = null;

    [SerializeField]
    private bool removeItem = false;

    [SerializeField]
    private Item giveItem = null;

    [SerializeField]
    private bool removeGameObject;

    [SerializeField]
    private Interaction replaceInteraction = null;

    [TextArea(15, 15)]
    public string Description;

    public string InteractionText { get => interactionText; set => interactionText = value; }
    public Item NeedItem { get => needsItem; set => needsItem = value; }
    public bool RemoveItem { get => removeItem; set => removeItem = value; }
    public Item GiveItem { get => giveItem; set => giveItem = value; }
    public bool RemoveGameObject { get => removeGameObject; set => removeGameObject = value; }
    public Interaction ReplaceInteraction { get => replaceInteraction; set => replaceInteraction = value; }

    public virtual void Interact(InteractionTarget target)
    {
        target.Interact(interactionType);
    }
}