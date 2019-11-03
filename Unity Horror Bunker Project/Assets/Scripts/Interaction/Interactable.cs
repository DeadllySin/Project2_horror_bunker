using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    public Interaction interaction = null;

    [SerializeField]
    private InteractionTarget target = null;

    [SerializeField]
    private bool needsForce = false;

    [SerializeField]
    private float needsForceAmount = 3f;

    [SerializeField]
    private float interactionCooldown = 0.5f;

    private InventoryManager inventoryManager;
    private float currentCooldown = 0;

    public bool NeedsForce { get => needsForce; set => needsForce = value; }
    public float NeedsForceAmount { get => needsForceAmount; set => needsForceAmount = value; }

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

    public void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= interactionCooldown * Time.deltaTime;
        }
    }

    public void Interact()
    {
        if (currentCooldown > 0 || interaction == null || (interaction.NeedItem != null && !inventoryManager.HasItem(interaction.NeedItem)))
        {
            return;
        }

        ManageInventory();

        InteractWithTarget();

        DoReplacements();
    }

    private void DoReplacements()
    {
        needsForce = interaction.NewInteractioNeedsForce;

        if (!string.IsNullOrEmpty(interaction.ReplaceTargetByName))
        {
            this.target = GameObject.Find(interaction.ReplaceTargetByName).GetComponent<InteractionTarget>();
        }

        if (interaction.ReplaceInteraction != null)
        {
            interaction = interaction.ReplaceInteraction;
        }
    }

    private void InteractWithTarget()
    {
        if (target != null)
        {
            interaction.Interact(target);
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

    public string GetToolTipText()
    {
        // TODO: UI
        return toolTipText;
    }
}