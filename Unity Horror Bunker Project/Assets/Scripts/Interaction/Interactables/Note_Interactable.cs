using System;
using UnityEngine;
using MyBox;

public class Note_Interactable : vp_Interactable
{
    [Space(10)]
    [Header("Custom")]

    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    private Note note = null;

    public override bool TryInteract(vp_PlayerEventHandler player)
    {
        if (!note)
        {
            return false;
        }

        if (!NotesManager.HasItem(note))
        {
            NotesManager.AddNote(note);
        }
        // TODO: To be removed:
        NotesManager.ShowNote(note);

        return true;
    }

}