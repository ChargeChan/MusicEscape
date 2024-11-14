using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawClick : MonoBehaviour
{
    private CamManager camManager;
    private bool isInJigsawMode = false;

    private GameObject selectedPiece;
    private Vector3 offset;
    private Camera puzzleCamera;

    void Start()
    {
        camManager = FindObjectOfType<CamManager>();
    }

    void Update()
    {
        if (!isInJigsawMode && camManager != null)
        {
            // Check which camera is active out of Cam13, Cam11, and Cam19
            Camera activeCamera = Camera.main;

            if (activeCamera != null)
            {

                // Only attempt to interact if in the initial view
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit = GetRaycastHit(activeCamera);
                    Debug.Log("Camera found: " + activeCamera);

                    if (hit.collider != null && hit.collider.CompareTag("Jigsaw"))
                    {
                        Debug.Log("Jigsaw piece clicked; switching to puzzle camera.");
                        camManager.SetActiveCamera(16);
                        isInJigsawMode = true;
                        puzzleCamera = camManager.GetCamera(16);
                    }
                }
            }
        }

        // If in jigsaw mode, allow dragging pieces
        if (isInJigsawMode)
        {
            HandlePieceMovement();
        }
    }

    private void HandlePieceMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = GetRaycastHit(puzzleCamera);
            if (hit.collider != null && hit.collider.CompareTag("Jigsaw"))
            {
                selectedPiece = hit.transform.gameObject;

                // Calculate the offset in world space and lock z-position
                offset = selectedPiece.transform.position - puzzleCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, puzzleCamera.WorldToScreenPoint(selectedPiece.transform.position).z));
                Debug.Log("Selected piece: " + selectedPiece.name);
            }
        }

        if (selectedPiece != null)
        {
            // Get the current mouse position and maintain the z-position
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, puzzleCamera.WorldToScreenPoint(selectedPiece.transform.position).z);
            Vector3 worldPosition = puzzleCamera.ScreenToWorldPoint(mousePosition);
            selectedPiece.transform.position = worldPosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            if (selectedPiece != null)
            {
                Debug.Log("Deselected piece: " + selectedPiece.name);
            }

            selectedPiece = null;
        }
    }




    private RaycastHit GetRaycastHit(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        return hit;
    }

    public void LeavePuzzle()
    {
        isInJigsawMode = false; // Reset the mode so the puzzle can be clicked again
        Debug.Log("Exited puzzle mode. Ready to re-enter.");
    }

}
