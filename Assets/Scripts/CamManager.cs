using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CamManager : MonoBehaviour
{
    public Camera[] cameras;
    public Camera pipeCamera;
    public Button leftArrow;
    public Button rightArrow;
    public Button forwardArrow;
    public Button backArrow;
    public Button Leave;
    public int defaultCameraDebug;

    private int currentCameraIndex = 0;
    private const int jigsawCameraIndex = 16;
    private JigsawClick jigsawClick;

    // Holds navigation options for each camera
    private Dictionary<int, CameraNavigation> cameraNavigationMap;

    void Start()
    {
        // Initialize the camera navigation map
        InitializeNavigationMap();

        // Set the first camera as active
        SetActiveCamera(defaultCameraDebug);
        UpdateUI();

        // Assign button functionality
        leftArrow.onClick.AddListener(MoveLeft);
        rightArrow.onClick.AddListener(MoveRight);
        forwardArrow.onClick.AddListener(MoveForward);
        backArrow.onClick.AddListener(MoveBackward);
        Leave.onClick.AddListener(MoveLeave);
        Leave.onClick.AddListener(LeavePuzzle);

        jigsawClick = FindObjectOfType<JigsawClick>();
    }

    public void LeavePuzzle()
    {
        if (jigsawClick != null)
        {
            jigsawClick.LeavePuzzle();  // Calls the LeavePuzzle() method from JigsawClick script
        }

    }


    void InitializeNavigationMap()
    {
        // Dictionary with each camera's available directions
        cameraNavigationMap = new Dictionary<int, CameraNavigation>
        {
            { 0, new CameraNavigation(forward: 6, left: 3, right: 2)},
            { 1, new CameraNavigation(back: 3)},
            { 2, new CameraNavigation(back: 0)},
            { 3, new CameraNavigation(back: 0)},
            { 4, new CameraNavigation(back: 3)},
            { 5, new CameraNavigation(back: 3)},
            { 6, new CameraNavigation(back: 0, right: 10, left: 9)},
            { 7, new CameraNavigation(back: 2)},
            { 8, new CameraNavigation(back: 6)},
            { 9, new CameraNavigation(back: 6)},
            {10,  new CameraNavigation(back: 6)},
            {11,  new CameraNavigation(left: 2, back: 12)},
            {12,  new CameraNavigation(back: 11, right: 2)},
            {13,  new CameraNavigation(back: 11)},
            {14,  new CameraNavigation(back: 11)},
            {15,  new CameraNavigation(back: 6, forward: 16, right: 4)},
            {16,  new CameraNavigation(leave: 13)},
            {17,  new CameraNavigation(back: 9)},
            {18,  new CameraNavigation(back: 9)},
            {19,  new CameraNavigation(forward: 0, left: 0)},
            {20,  new CameraNavigation(forward: 0, right: 0)},
            {21,  new CameraNavigation(back: 22)},
            {22,  new CameraNavigation(forward: 21)},
            {23,  new CameraNavigation(back: 8)}
            // Continue defining mappings for each camera...
        };
        //for testing sample scene
        if(SceneManager.GetActiveScene().name == "SampleScene")
        cameraNavigationMap = new Dictionary<int, CameraNavigation>
        {
            { 0, new CameraNavigation(left: 2, right:3)},
            { 1, new CameraNavigation(back: 0)},
            { 2, new CameraNavigation(right: 0)},
            { 3, new CameraNavigation(left: 0)},
        };
    }

    public void SetActiveCamera(int index)
    {
        // Deactivate all cameras
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
            cameras[i].gameObject.GetComponent<AudioListener>().enabled = (i== index);
            cameras[i].tag = "Untagged";
        }
        currentCameraIndex = index;
        GameManager.Instance.SetCurrentCameraIndex(index);
        cameras[index].tag = "MainCamera";

        // Show or hide UI elements based on the camera index
        if (index == jigsawCameraIndex) // Cam21
        {
            // Only show the Leave button and hide other UI elements
            ToggleUI(false); // Hide navigation UI elements
            ShowLeaveButton(true); // Show the Leave button
        }
        else
        {
            // Show main UI for other cameras
            ShowLeaveButton(false); // Hide the Leave button
            ToggleUI(true); // Show main UI
            UpdateUI();
        }


    }

    public Camera GetCamera(int cameraIndex)
    {
        // Return the camera at the specified index if it's valid
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            return cameras[cameraIndex];
        }
        else
        {
            Debug.LogError("Camera index out of range!");
            return null;
        }

    }

    void ToggleUI(bool show)
    {
        leftArrow.gameObject.SetActive(show);
        rightArrow.gameObject.SetActive(show);
        forwardArrow.gameObject.SetActive(show);
        backArrow.gameObject.SetActive(show);
    }

    void UpdateUI()
    {
        // Enable or disable buttons based on available directions from current camera
        CameraNavigation nav = cameraNavigationMap[currentCameraIndex];
        forwardArrow.gameObject.SetActive(nav.forward != -1);
        backArrow.gameObject.SetActive(nav.back != -1);
        leftArrow.gameObject.SetActive(nav.left != -1);
        rightArrow.gameObject.SetActive(nav.right != -1);
    }

    public void ShowLeaveButton(bool isVisible)
    {
        Leave.gameObject.SetActive(isVisible); // Make the Leave button visible or hidden
    }


    // Movement functions that check for valid navigation options
    void MoveLeft()
    {
        int targetCamera = cameraNavigationMap[currentCameraIndex].left;
        if (targetCamera != -1) SetActiveCamera(targetCamera);
        UpdateUI();
    }

    void MoveRight()
    {
        int targetCamera = cameraNavigationMap[currentCameraIndex].right;
        if (targetCamera != -1) SetActiveCamera(targetCamera);
        UpdateUI();
    }

    void MoveForward()
    {
        int targetCamera = cameraNavigationMap[currentCameraIndex].forward;
        if (targetCamera != -1) SetActiveCamera(targetCamera);
        UpdateUI();
    }

    void MoveBackward()
    {
        int targetCamera = cameraNavigationMap[currentCameraIndex].back;
        if (targetCamera != -1) SetActiveCamera(targetCamera);
        UpdateUI();
        //re enable colliders
        GameManager.Instance.EnableCurrentCameraObjectCollider();
    }

    void MoveLeave()
    {
        int targetCamera = cameraNavigationMap[currentCameraIndex].leave;
        if (targetCamera != -1) SetActiveCamera(targetCamera);
        UpdateUI();
        
    }


    IEnumerator WaitToRenderPipe()
    {
        yield return new WaitForEndOfFrame();
        pipeCamera.enabled = false;
        yield return new WaitForEndOfFrame();
        pipeCamera.enabled = true;
        pipeCamera.Render();
    }

    public int GetCurrentCameraIndex()
    {
        return currentCameraIndex;
    }
}

// Helper class to store navigation options for each camera
public class CameraNavigation
{
    public int left, right, forward, back, leave;

    public CameraNavigation(int left = -1, int right = -1, int forward = -1, int back = -1, int leave = -1)
    {
        this.left = left;
        this.right = right;
        this.forward = forward;
        this.back = back;
        this.leave = leave;
    }
}


