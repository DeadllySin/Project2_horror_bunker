using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Note", menuName = "Notes system/new Note")]
public class Note : ScriptableObject
{
    [SerializeField] string label = string.Empty;
    public string Label { get { return label; } }

    [SerializeField] Page[] pages = new Page[0];
    public Page[] Pages { get { return pages; } }
}