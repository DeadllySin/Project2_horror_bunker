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



    public override void Interact(params object[] values)
    {
        if (open)
        {
            Close();
        }
        else
        {
            Open();
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
        if (locked)
        {
            // TODO: Play locked door sound
            return;
        }

        open = true;
        animator.SetBool("open", open);
        if (autoClose)
        {
            StartCoroutine("AutoClose");
        }
    }

    public void Close()
    {
        StopCoroutine("AutoClose");
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
        yield return new WaitForSeconds(autoCloseDelay);
        Close();
    }

}