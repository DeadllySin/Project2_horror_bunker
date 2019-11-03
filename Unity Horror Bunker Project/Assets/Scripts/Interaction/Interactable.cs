using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    public Interaction interaction = null;

    [SerializeField]
    private InteractionTarget target = null;

    private InventoryManager inventoryManager;

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
    }

    public void Interact()
    {
        if (interaction == null || (interaction.NeedItem != null && !inventoryManager.HasItem(interaction.NeedItem)))
        {
            return;
        }

        ManageInventory();

        if (target != null)
        {
            interaction.Interact(target);

            if (interaction.RemoveGameObject)
            {
                Destroy(target.transform.gameObject);
            }
        }
        else if (interaction.RemoveGameObject)
        {
            Destroy(this.transform.gameObject);
        }

        interaction = interaction.ReplaceInteraction;
    }

    private void ManageInventory()
    {
        if (interaction.RemoveItem)
        {
            inventoryManager.RemoveItem(interaction.NeedItem);
        }

        if (interaction.GiveItem != null)
        {
            inventoryManager.AddItem(interaction.GiveItem);
        }
    }

    public string GetToolTipText()
    {
        // TODO: UI
        return toolTipText;
    }
}