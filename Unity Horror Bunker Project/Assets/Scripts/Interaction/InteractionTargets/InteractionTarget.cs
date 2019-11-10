using UnityEngine;

public abstract class InteractionTarget : MonoBehaviour
{
    public abstract void Interact(Interaction.InteractionType interactionType, string value = null);
}