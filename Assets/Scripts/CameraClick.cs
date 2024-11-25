using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{
    public int targetCameraIndex; // The index of the camera to switch to when clicked
    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        //if (!this.enabled) return;
        // Find CamManager and switch to the target camera
        CamManager camManager = FindObjectOfType<CamManager>();
        if (camManager != null)
        {
            camManager.SetActiveCamera(targetCameraIndex);
            myCollider.enabled = false;
            GameManager.Instance.SetCurrentCameraObjectCollider(myCollider);
        }
        else
        {
            Debug.LogError("CamManager not found in the scene.");
        }
    }
}
