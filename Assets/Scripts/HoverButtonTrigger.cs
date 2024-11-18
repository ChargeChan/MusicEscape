using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButtonTrigger : MonoBehaviour
{
    public Button hoverButton; // Assign your button here
    public int targetCameraIndex = 11;

    private void Start()
    {
        // Ensure the button is hidden initially
        hoverButton.gameObject.SetActive(false);
        hoverButton.onClick.AddListener(SwitchToTargetCamera);
    }

    void Update()
    {
        // Check if the mouse is over the spot
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                hoverButton.gameObject.SetActive(true); // Show the button
            }
            else
            {
                hoverButton.gameObject.SetActive(false); // Hide the button if not hovering
            }
        }
        else
        {
            hoverButton.gameObject.SetActive(false); // Hide the button if not hovering
        }
    }

    void SwitchToTargetCamera()
    {
        // Find the CamManager and switch to the target camera
        CamManager camManager = FindObjectOfType<CamManager>();
        if (camManager != null)
        {
            camManager.SetActiveCamera(targetCameraIndex);
        }
        else
        {
            Debug.LogError("CamManager not found in the scene.");
        }
    }

}
