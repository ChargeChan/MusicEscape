using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperScript : MonoBehaviour
{
    public string noteContent; 
    public Canvas noteCanvas; 
    public Image noteImage; 

    private bool isNoteActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isNoteActive)
        {
            // Check for clicks outside the note to close it
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!noteCanvas.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    CloseNote();
                }
            }
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
        // Show the note canvas and set the content
        noteCanvas.gameObject.SetActive(true);
        noteCanvas.GetComponentInChildren<Text>().text = noteContent;

        // If there is an image for the note, enable it
        if (noteImage != null)
        {
            noteImage.gameObject.SetActive(true);
        }

        isNoteActive = true;
    }

    public void CloseNote()
    {
        // Hide the note canvas
        noteCanvas.gameObject.SetActive(false);
        isNoteActive = false;

        // Optionally hide the image when the note is closed
        if (noteImage != null)
        {
            noteImage.gameObject.SetActive(false);
        }
    }
}