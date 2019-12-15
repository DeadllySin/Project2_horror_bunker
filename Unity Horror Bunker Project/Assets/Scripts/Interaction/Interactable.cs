using System;
using UnityEngine;
using MyBox;

public class Interactable : vp_Interactable
{
    private enum InteractionType
    {
        Item,
        Note,
        Button,
        Complex
    }

    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    private InteractionType interactionType = InteractionType.Item;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Item)]
    private Item giveItem = null;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Note)]
    private Note openNote = null;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Button, InteractionType.Complex)]
    private GameObject target = null;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Complex)]
    public Interaction interaction = null;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Button)]
    public string targetMethod = "Default";

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Button)]
    public string targetValue = null;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Button, InteractionType.Complex)]
    private bool needsForce = false;

    [SerializeField, ConditionalField("needsForce")]
    private float needsForceAmount = 3.0f;

    [SerializeField, ConditionalField("needsForce")]
    private float forceAddPerInteraction = 0.2f;

    [SerializeField, ConditionalField("needsForce")]
    private float forceReleasePerSecond = 2.5f;

    [SerializeField, ConditionalField("interactionType", false, InteractionType.Button, InteractionType.Complex)]
    private float interactionCooldown = 0.0f;

    private bool forceBuildUp = false;
    public float currentForce = 0;
    private float currentCooldown = 0.0f;

    public bool NeedsForce { get => needsForce; }
    public float NeedsForceAmount { get => needsForceAmount; }
    public string ToolTipText { get => toolTipText; }

    protected override void Start()
    {
        // The target is used for special functionality (e.g. DoorInteractableTarget).
        // If no target is given, a target attached to the same object will be used.
        // If no target is present at all, target specific functionality will not be executed.

        base.Start();

        if (target == null)
        {
            target = this.transform.gameObject;
        }
    }

    public void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= interactionCooldown * Time.deltaTime;
        }

        if (forceBuildUp)
        {
            ReleaseForce();
        }
    }

    public override bool TryInteract(vp_PlayerEventHandler player)
    {
        if (!BuildUpForce() || currentCooldown > 0)
        {
            return false;
        }

        switch (interactionType)
        {
            case InteractionType.Item:
                InventoryManager.AddItem(giveItem);
                Destroy(this.transform.gameObject);
                break;
            case InteractionType.Note:
                if (!NotesManager.HasItem(openNote))
                {
                    NotesManager.AddNote(openNote);
                }
                // TODO: To be removed:
                NotesManager.ShowNote(openNote);
                break;
            case InteractionType.Button:
                if (string.IsNullOrEmpty(targetValue))
                {
                    target.SendMessage(targetMethod, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    target.SendMessage(targetMethod, targetValue, SendMessageOptions.DontRequireReceiver);
                }
                break;
            case InteractionType.Complex:
                if (interaction == null || (interaction.NeedItem != null && !InventoryManager.HasItem(interaction.NeedItem)))
                {
                    return false;
                }

                ManageInventory();
                ComplexInteract();
                DoReplacements();
                break;

        }
        currentCooldown = interactionCooldown;
        return true;
    }
    private void DoReplacements()
    {
        needsForce = interaction.NewInteractionNeedsForce;

        if (interaction.ReplaceTarget != null)
        {
            this.target = interaction.ReplaceTarget;
        }

        if (interaction.ReplaceInteraction != null)
        {
            interaction = interaction.ReplaceInteraction;
        }
    }

    private void ComplexInteract()
    {
        if (target != null)
        {
            if (string.IsNullOrEmpty(interaction.InteractionValue))
            {
                target.SendMessage(interaction.InteractionMethod, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                target.SendMessage(interaction.InteractionMethod, targetValue, SendMessageOptions.DontRequireReceiver);
            }

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
            InventoryManager.RemoveItem(interaction.NeedItem);
        }

        if (interaction.GiveItem != null)
        {
            InventoryManager.AddItem(interaction.GiveItem);
        }

        if (interaction.GiveNote != null)
        {
            NotesManager.AddNote(interaction.GiveNote);
        }
    }
    private bool BuildUpForce()
    {
        if (!needsForce)
        {
            return true;
        }

        forceBuildUp = true;
        currentForce += forceAddPerInteraction;
        if (currentForce >= needsForceAmount)
        {
            // interactable.TryInteract(this.gameObject);
            currentForce = 0f;
            forceBuildUp = false;
            needsForce = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void ReleaseForce()
    {
        currentForce -= forceReleasePerSecond * Time.deltaTime;
        if (currentForce <= 0)
        {
            currentForce = 0f;
            forceBuildUp = false;
        }
    }
}