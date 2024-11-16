using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    public int targetCameraIndex; // The index of the camera to switch to when clicked

    void OnMouseDown()
    {
        // Find CamManager and switch to the target camera
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
