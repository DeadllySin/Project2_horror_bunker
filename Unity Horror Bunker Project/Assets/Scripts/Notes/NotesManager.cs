using System.Collections.Generic;
using UnityEngine;

public static class NotesManager
{
    public static List<Note> Diary;

    public static void Initialize()
    {
        Diary = new List<Note>();
    }

    public static void AddNote(Note note)
    {
        Diary.Add(note);
        // TODO: show a tooltip "Press J to read note" or something like that
    }

    public static void RemoveNote(Note note)
    {
        Diary.Remove(note);
    }

    public static bool HasItem(Note note)
    {
        return Diary.Contains(note);
    }

    public static void ShowNote(Note note)
    {
        // TODO: Show a note UI with note preselected
        UIManager.ShowNote(note);
    }
}
