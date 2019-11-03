using System.Collections;
using UnityEngine;

public class DoorInteractionTarget : InteractionTarget
{
    [SerializeField]
    private bool open;

    [SerializeField]
    private bool locked;

    [SerializeField]
    private bool autoClose = false;

    [SerializeField]
    private float autoCloseDelay = 5f;

    [SerializeField]
    private Animator animator = null;

    public override void Interact(Interaction.InteractionType interactionType)
    {
        switch (interactionType)
        {
            case Interaction.InteractionType.Default:
                if (open)
                {
                    Close();
                }
                else
                {
                    Open();
                }
                break;

            case Interaction.InteractionType.Open:
                Open();
                break;

            case Interaction.InteractionType.Close:
                Close();
                break;

            case Interaction.InteractionType.Lock:
                Lock();
                break;

            case Interaction.InteractionType.Unlock:
                Unlock();
                break;

            default:
                Debug.Log("Confused door..." + this.gameObject.ToString());
                break;
        }
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    public void Open()
    {
        open = true;
        animator.SetBool("open", open);
        if (autoClose)
        {
            StartCoroutine("AutoClose");
        }
    }

    public void Close()
    {
        open = false;
        animator.SetBool("open", open);
    }

    public string GetState()
    {
        if (locked)
        {
            return "locked";
        }
        else if (open)
        {
            return "open";
        }
        else
        {
            return "closed";
        }
    }

    private IEnumerator AutoClose()
    {
        // TODO: Stop Coroutine when players closes the door manually.
        yield return new WaitForSeconds(autoCloseDelay);
        Close();
    }
}