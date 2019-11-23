using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager
{
    private static GameObject UIBase = null;
    private static NoteUI noteUI = null;
    
    public static void Initialize()
    {
        // Activate all children, so that their components can be found

        UIBase = GameObject.FindGameObjectWithTag("UIBase");
        for (int i = 0; i < UIBase.transform.childCount; i++)
        {
            UIBase.transform.GetChild(i).gameObject.SetActive(true);
        }

        // Register UI components
        noteUI = UIBase.GetComponentInChildren<NoteUI>();
        if (noteUI == null)
        {
            Debug.Log("Can´t finde NoteUI!");
        }
        noteUI.gameObject.SetActive(false);
    }

    public static void ShowNote(Note note)
    {
        vp_LocalPlayer.HideCrosshair();
        vp_LocalPlayer.DisableGameplayInput();
        vp_LocalPlayer.DisableFreeLook();
        vp_LocalPlayer.ShowMouseCursor();
        noteUI.gameObject.SetActive(true);
        noteUI.ShowNote(note);
    }

    public static void CloseNote()
    {
        noteUI.gameObject.SetActive(false);
        vp_LocalPlayer.ShowCrosshair();
        vp_LocalPlayer.EnableGameplayInput();
        vp_LocalPlayer.EnableFreeLook();
        vp_LocalPlayer.HideMouseCursor();
    }
}
