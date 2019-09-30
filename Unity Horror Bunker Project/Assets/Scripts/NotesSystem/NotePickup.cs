using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour, IInteractable
{
    [SerializeField] Note note = null;

    [SerializeField] bool autoDisplay = false;
    
    public void Interact()
    {
        if (autoDisplay)
        {
            NotesSystem.Display(note);
        }
    }

}
