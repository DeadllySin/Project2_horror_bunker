using System;
using UnityEngine;
using MyBox;

public class Complex_Interactable : vp_Interactable
{

    [Space(10)]
    [Header("Custom")]

    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private Interaction interaction = null;

    public override bool TryInteract(vp_PlayerEventHandler player)
    {
        if (!target || !interaction)
        {
            return false;
        }


        if (HasNeededItems())
        {
            target.GetComponent<InteractionTarget>().Interact(interaction.GetInteractionValues());

            RemoveItems();
            RecieveItems();

            UpdateTarget();
        }

        return true;
    }

    private bool HasNeededItems()
    {

        foreach (InteractableItem item in interaction.neededItems)
        {
            if (item is Item)
            {
                if (!InventoryManager.HasItem(item as Item))
                {
                    return false;
                }
            }

            if (item is Note)
            {
                if (!NotesManager.HasItem(item as Note))
                {
                    return false;
                }
            }
        }

        return true;

    }

    private void RemoveItems()
    {
        foreach (InteractableItem item in interaction.removedItems)
        {
            if (item is Item)
            {
                InventoryManager.RemoveItem(item as Item);
            }
            if (item is Note)
            {
                NotesManager.AddNote(item as Note);
            }
        }
    }

    private void RecieveItems()
    {
        foreach (InteractableItem item in interaction.removedItems)
        {
            if (item is Item)
            {
                InventoryManager.AddItem(item as Item);
            }
        }
    }


    private void UpdateTarget()
    {
        if (interaction.removeTarget)
        {
            Destroy(target);
        }
    }
}