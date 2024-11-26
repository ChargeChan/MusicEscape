using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaperScript : MonoBehaviour
{
    public string noteContent;
    public Canvas noteCanvas;
    public Image noteImage;
    private TextMeshProUGUI noteText;

    private bool isNoteActive = false;

    void Start()
    {
        // Ensure the canvas is disabled at the start
        if (noteCanvas != null)
        {
            noteCanvas.gameObject.SetActive(false);

            // Find the TMP Text component within the canvas
            noteText = noteCanvas.GetComponentInChildren<TextMeshProUGUI>();
            if (noteText == null)
            {
                Debug.LogError("TMP TextMeshProUGUI component not found in NoteCanvas!");
            }
        }
        else
        {
            Debug.LogError("NoteCanvas is not assigned!");
        }
    }

    void OnMouseDown()
    {
        if (!isNoteActive)
        {
            OpenNote();
        }
    }

    public void OpenNote()
    {
        if (noteCanvas != null)
        {
            noteCanvas.gameObject.SetActive(true);

            if (noteText != null)
            {
                noteText.text = noteContent;
            }
        }

        if (noteImage != null)
        {
            noteImage.gameObject.SetActive(true);
        }

        isNoteActive = true;
    }

    public void CloseNote()
    {
        if (noteCanvas != null)
        {
            noteCanvas.gameObject.SetActive(false);
        }
        isNoteActive = false;
    }
}