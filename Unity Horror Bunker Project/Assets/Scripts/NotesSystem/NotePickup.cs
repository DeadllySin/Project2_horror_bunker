using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour, IInteractable
{
    [SerializeField] Note note = null;

    bool autoDisplay = true;

    public void Interact()
    {
        if (autoDisplay)
        {
            NotesSystem.Display(note);
        }
    }
}
