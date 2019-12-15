using UnityEngine;

public abstract class InteractionTarget : MonoBehaviour
{
    public abstract void Interact(params object[] values);
}