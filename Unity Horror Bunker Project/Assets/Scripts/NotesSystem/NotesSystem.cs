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

    }
    private void OnDisable()
    {

    }
    private void Start()
    {

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

    private void UpdateUI()
    {
        UI.PreviousButton.interactable = !(currentPage == 0);
        UI.NextButton.interactable = !(currentPage == activeNote.Pages.Length - 1);

        var useSubscript = ActivePage.Type == PageType.Texture && ActivePage.UseSubscript;
        UI.ReadButton.alpha = useSubscript ? (readSubscript ? .5f : 1f) : 0f;
        UpdateCanvasGroup(readSubscript, UI.SubscriptGroup);
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
}