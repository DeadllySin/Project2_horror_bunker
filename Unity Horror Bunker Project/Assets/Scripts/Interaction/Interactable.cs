using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private enum InteractionType
    {
        Item,
        Note,
        Interaction,
        SendValue
    }
    
    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    private InteractionType interactionType = InteractionType.Item;

    [SerializeField]
    private Item giveItem = null;

    [SerializeField]
    private Note openNote = null;

    [SerializeField]
    public Interaction interaction = null;

    [SerializeField]
    public string valueToSend = null;

    [SerializeField]
    private InteractionTarget target = null;

    [SerializeField]
    private bool needsForce = false;

    [SerializeField]
    private float needsForceAmount = 3.0f;

    [SerializeField]
    private float interactionCooldown = 0.0f;

    private InventoryManager inventoryManager;
    private NotesManager notesManager;
    private float currentCooldown = 0.0f;

    public bool NeedsForce { get => needsForce;}
    public float NeedsForceAmount { get => needsForceAmount;}
    public string ToolTipText { get => toolTipText;}

    public void Start()
    {
        // The target is used for special functionality (e.g. DoorInteractableTarget).
        // If no target is given, a target attached to the same object will be used.
        // If no target is present at all, target specific functionality will not be executed.

        if (target == null)
        {
            target = GetComponent<InteractionTarget>();
        }
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
        notesManager = GameObject.Find("Notes Manager").GetComponent<NotesManager>();
    }

    public void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= interactionCooldown * Time.deltaTime;
        }
    }

    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Item:
                inventoryManager.AddItem(giveItem);
                Destroy(this.transform.gameObject);
                break;
            case InteractionType.Note:
                if (!notesManager.HasItem(openNote))
                {
                    notesManager.AddNote(openNote);
                }
                notesManager.ShowNote(openNote);
                break;
            case InteractionType.Interaction:
                if (currentCooldown > 0 || interaction == null || (interaction.NeedItem != null && !inventoryManager.HasItem(interaction.NeedItem)))
                {
                    // Play interaction fail sound?
                    return;
                }

                ManageInventory();
                InteractWithTarget();
                DoReplacements();
                break;
            case InteractionType.SendValue:
                target.Interact(Interaction.InteractionType.Default, valueToSend);
                break;
        }
    }

    private void DoReplacements()
    {
        needsForce = interaction.NewInteractioNeedsForce;

        // TODO: Find a better way to replace targets, since multiple GameObjects can be named the same.

        if (!string.IsNullOrEmpty(interaction.ReplaceTargetByName))
        {
            this.target = GameObject.Find(interaction.ReplaceTargetByName).GetComponent<InteractionTarget>();
        }

        if (interaction.ReplaceInteraction != null)
        {
            interaction = interaction.ReplaceInteraction;
        }
    }

    private void InteractWithTarget(string value = null)
    {
        if (target != null)
        {
            interaction.Interact(target, value);
            currentCooldown = interactionCooldown;

            if (interaction.RemoveTargetFromWorld)
            {
                Destroy(target.transform.gameObject);
            }
        }
        else if (interaction.RemoveTargetFromWorld)
        {
            Destroy(this.transform.gameObject);
        }
    }

    private void ManageInventory()
    {
        if (interaction.RemoveNeededItem && interaction.NeedItem != null)
        {
            inventoryManager.RemoveItem(interaction.NeedItem);
        }

        if (interaction.GiveItem != null)
        {
            inventoryManager.AddItem(interaction.GiveItem);
        }
    }
}