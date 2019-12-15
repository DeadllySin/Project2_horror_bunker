using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Collectibles/Note")]

public class Note : InteractableItem
{
    public enum NoteTypes
    {
        TextPages,
        TextScrolling,
        Image
    }
    
    [SerializeField]
    private string noteName = null;

    [SerializeField]
    private NoteTypes noteType = NoteTypes.TextPages;

    [SerializeField]
    private Sprite background = null;

    [SerializeField]
    private bool useHTML = true;

    [SerializeField]
    [TextArea(25, 15)]
    private string noteText = null;

    public string NoteName { get => noteName; set => noteName = value; }
    public NoteTypes NoteType { get => noteType; set => noteType = value; }
    public Sprite Background { get => background; set => background = value; }
    public string NoteText { get => noteText; set => noteText = value; }
    public bool UseHTML { get => useHTML; set => useHTML = value; }
}
