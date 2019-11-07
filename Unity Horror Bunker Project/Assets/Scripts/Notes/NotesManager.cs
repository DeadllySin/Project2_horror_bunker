using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    [SerializeField]
    private UIManager myUIManager = null;
    
    public List<Note> Diary;

    public void AddNote(Note note)
    {
        Diary.Add(note);
    }

    public void RemoveNote(Note note)
    {
        Diary.Remove(note);
    }

    public bool HasItem(Note note)
    {
        return Diary.Contains(note);
    }

    public void ShowNote(Note note)
    {
        myUIManager.ShowNote(note);
    }
}
