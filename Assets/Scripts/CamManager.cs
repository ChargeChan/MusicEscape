using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamManager : MonoBehaviour
{
    public Camera[] cameras;
    public Camera pipeCamera;
    public Button leftArrow;
    public Button rightArrow;
    public Button forwardArrow;
    public Button backArrow;

    private int currentCameraIndex = 0;

    // Holds navigation options for each camera
    private Dictionary<int, CameraNavigation> cameraNavigationMap;

    void Start()
    {
        // Initialize the camera navigation map
        InitializeNavigationMap();

        // Set the first camera as active
        SetActiveCamera(21);
        UpdateUI();

        // Assign button functionality
        leftArrow.onClick.AddListener(MoveLeft);
        rightArrow.onClick.AddListener(MoveRight);
        forwardArrow.onClick.AddListener(MoveForward);
        backArrow.onClick.AddListener(MoveBackward);
    }

    void InitializeNavigationMap()
    {
        // Dictionary with each camera's available directions
        cameraNavigationMap = new Dictionary<int, CameraNavigation>
        {
            { 0, new CameraNavigation(forward: 3, left: 1, right: 2)},
            { 1, new CameraNavigation(right: 3)},
            { 2, new CameraNavigation(forward: 11, left: 3)},
            { 3, new CameraNavigation(back: 16, forward: 6, left: 4, right: 7)},
            { 4, new CameraNavigation(back: 3, left: 1, right: 5)},
            { 5, new CameraNavigation(back: 6, left: 4)},
            { 6, new CameraNavigation(back: 15, forward: 18, left: 5, right: 7)},
            { 7, new CameraNavigation(back: 6)},
            { 8, new CameraNavigation(back: 17, left: 9, right: 10)},
            { 9, new CameraNavigation(back: 17, left: 17)},
            {10,  new CameraNavigation(left: 8, right: 17)},
            {11,  new CameraNavigation(forward: 19, left: 0)},
            {12,  new CameraNavigation(back: 11, right: 0)},
            {13,  new CameraNavigation(back: 19, left: 12)},
            {14,  new CameraNavigation(right: 20)},
            {15,  new CameraNavigation(back: 6, forward: 16, right: 4)},
            {16,  new CameraNavigation(back: 0, left: 2, right: 1)},
            {17,  new CameraNavigation(back: 18, forward: 15, left: 7, right: 5)},
            {18,  new CameraNavigation(back: 17, forward: 8, left: 9)},
            {19,  new CameraNavigation(forward: 14, left: 13)},
            {20,  new CameraNavigation(forward: 12, right: 13)},
            {21,  new CameraNavigation(back: 22)},
            {22,  new CameraNavigation(forward: 21)}
            // Continue defining mappings for each camera...
        };
    }

    void SetActiveCamera(int index)
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
    public int left, right, forward, back;

    public CameraNavigation(int left = -1, int right = -1, int forward = -1, int back = -1)
    {
        this.left = left;
        this.right = right;
        this.forward = forward;
        this.back = back;
    }
}


