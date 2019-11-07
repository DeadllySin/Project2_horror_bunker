using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image crosshair = null;
    
    [SerializeField]
    private Image crosshairInteract = null;

    [SerializeField]
    private NoteUI noteUI = null;

    private InputManager myInputManager = null;
        
    // Start is called before the first frame update
    void Start()
    {
        CrossHair(true);
        noteUI.gameObject.SetActive(false);
        myInputManager = GameObject.FindObjectOfType<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CrossHair(bool showCrosshair)
    {
        crosshair.enabled = showCrosshair;
    }

    public void CrosshairInteract(bool interactCrosshair)
    {
        crosshairInteract.enabled = interactCrosshair;
    }

    public void ShowNote(Note note)
    {
        myInputManager.StopPlayerMovement();
        noteUI.gameObject.SetActive(true);
        noteUI.ShowNote(note);
    }

    public void CloseNote()
    {
        noteUI.gameObject.SetActive(false);
        myInputManager.StartPlayerMovement();
    }
}
