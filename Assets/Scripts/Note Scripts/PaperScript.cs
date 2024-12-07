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
    public Sprite noteSprite;
    private TextMeshProUGUI noteText;
    private SkullPuzzleManager skullPuzzleManager;

    public Button exitNote;
    public AudioSource audioFX;

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

        skullPuzzleManager = FindObjectOfType<SkullPuzzleManager>();
        exitNote.onClick.AddListener(CloseNote);
        audioFX = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!isNoteActive)
        {
            audioFX.Play();
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
            if (noteSprite != null) // Use the assigned sprite
            {
                noteImage.sprite = noteSprite;
                noteImage.gameObject.SetActive(true); // Show the image if a sprite is assigned
            }
            else
            {
                noteImage.gameObject.SetActive(false); // Hide the image if no sprite is assigned
            }
        }

        isNoteActive = true;
        skullPuzzleManager.ActivatePuzzle();
    }

    public void CloseNote()
    {
        if (noteCanvas != null)
        {
            noteCanvas.gameObject.SetActive(false);
        }

        if (noteImage != null)
        {
            noteImage.gameObject.SetActive(false);
        }

        isNoteActive = false;
        Debug.Log($"Note closed for {gameObject.name}. isNoteActive reset to {isNoteActive}");
    }
}