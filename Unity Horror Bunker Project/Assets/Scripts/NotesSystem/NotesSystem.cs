using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIElements
{
    [SerializeField] TextMeshProUGUI textObj;
    public TextMeshProUGUI TextObj { get { return textObj; } }

    [SerializeField] TextMeshProUGUI subscriptText;
    public TextMeshProUGUI SubscriptText { get { return subscriptText; } }

    [SerializeField] CanvasGroup subscriptGroup;
    public CanvasGroup SubscriptGroup { get { return subscriptGroup; } }

    [SerializeField] Image page;
    public Image Page { get { return page; } }

    [SerializeField] CanvasGroup noteCanvasGroup;
    public CanvasGroup NoteCanvasGroup { get { return noteCanvasGroup; } }

    [SerializeField] CanvasGroup readButton;
    public CanvasGroup ReadButton { get { return readButton; } }

    [SerializeField] CanvasGroup nextButton;
    public CanvasGroup NextButton { get { return nextButton; } }

    [SerializeField] CanvasGroup previousButton;
    public CanvasGroup PreviousButton { get { return previousButton; } }




}

public class NotesSystem : MonoBehaviour
{
    #region Data and Actions
    [SerializeField] UIElements UI = new UIElements();

    private Action<Note> A_Display = delegate { };

    #endregion

    #region Privates and Properties
    private Note activeNote = null;
    private Page ActivePage
    {
        get
        {
            return activeNote.Pages[currentPage];
        }
    }
    private int currentPage = 0;
    private bool readSubscript = false;
    private Sprite defaultPageTexture = null;
    private bool usingNotesSytem = false;

    #endregion

    #region Unity's default methods
    private void OnEnable()
    {
        A_Display += DisplayNote;
    }
    private void OnDisable()
    {
        A_Display -= DisplayNote;
    }
    private void Start()
    {
        Close();

        defaultPageTexture = UI.Page.sprite;
    }
    private void Update()
    {
    }
    #endregion

    public void Open()
    {
        //disable character controller
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void Close()
    {
        //enable character controller
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        CloseNote();
        UpdateCanvasGroup(false, UI.NoteCanvasGroup);
    }

    private void DisplayNote(Note note)
    {
        UpdateCanvasGroup(true, UI.NoteCanvasGroup);
        activeNote = note;

        DisplayPage(0);
    }

    private void DisplayPage(int page)
    {
        UI.ReadButton.interactable = activeNote.Pages[page].Type == PageType.Texture;

        if (activeNote.Pages[page].Type != PageType.Texture)
            readSubscript = false;
        else
            if (readSubscript == true)
                UpdateSubscript();


        switch (activeNote.Pages[page].Type)
        {
            case PageType.Text:
                UI.Page.sprite = defaultPageTexture;
                UI.TextObj.text = activeNote.Pages[page].Text;
                break;
            case PageType.Texture:
                UI.Page.sprite = activeNote.Pages[page].Texture;
                UI.TextObj.text = string.Empty;
                break;
        }
        UpdateUI();
    }

    public void CloseNote()
    {
        UpdateCanvasGroup(false, UI.NoteCanvasGroup);
        OnNoteClose();
    }

    private void UpdateUI()
    {
        UI.PreviousButton.interactable = !(currentPage == 0);
        UI.NextButton.interactable = !(currentPage == activeNote.Pages.Length - 1);

        var useSubscript = ActivePage.Type == PageType.Texture && ActivePage.UseSubscript;
        UI.ReadButton.alpha = useSubscript ? (readSubscript ? .5f : 1f) : 0f;
        UpdateCanvasGroup(readSubscript, UI.SubscriptGroup);
    }

    private void UpdateSubscript()
    {
        UI.SubscriptText.text = readSubscript ? ActivePage.Text : string.Empty;
    }

    public void Next()
    {
        currentPage++;

        DisplayPage(currentPage);
    }

    public void Previous()
    {
        currentPage--;

        DisplayPage(currentPage);
    }

    public void Read()
    {
        readSubscript = !readSubscript;

        UpdateSubscript();
        UpdateUI();
    }

    private void UpdateCanvasGroup(bool state, CanvasGroup canvasGroup)
    {
        switch (state)
        {
            case true:
                canvasGroup.alpha = 1.0f;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
                break;
            case false:
                canvasGroup.alpha = 0.0f;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
                break;
        }
    }

    private void OnNoteClose()
    {
        activeNote = null;
        currentPage = 0;
        readSubscript = false;
    }
}