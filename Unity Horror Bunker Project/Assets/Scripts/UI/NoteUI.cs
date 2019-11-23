using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI noteText = null;

    [SerializeField]
    private Image noteBackground = null;

    [SerializeField]
    private Button previousButton = null;

    [SerializeField]
    private Button nextButton = null;

    private int pageCount = 0;
    private int currentPage = 0;

    public void ShowNote(Note note)
    {
        noteBackground.sprite = note.Background;
        noteText.rectTransform.eulerAngles = new Vector3(0, 0, Random.Range(-1f, 1f));
        noteText.SetText(note.NoteText);
        noteText.ForceMeshUpdate();
        pageCount = noteText.textInfo.pageCount;
        
        currentPage = 1;
        SetNavigationButtons();
    }

    private void SetNavigationButtons()
    {
        previousButton.gameObject.SetActive(!(currentPage == 1));
        nextButton.gameObject.SetActive(currentPage < pageCount);
    }

    public void nextPage()
    {
        currentPage++;
        UpdatePage();
    }

    public void previousPage()
    {
        currentPage--;
        UpdatePage();
    }
    private void UpdatePage()
    {
        noteText.pageToDisplay = currentPage;
        noteText.ForceMeshUpdate();
        SetNavigationButtons();
    }

    public void CloseNote()
    {
        UIManager.CloseNote();
    }
}
