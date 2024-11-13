using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCameraScript : MonoBehaviour
{
    Camera pipeCamera;
    // Start is called before the first frame update
    void Start()
    {
        pipeCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        StartCoroutine(DisplayCameraHelper());
    }

    public void Hide()
    {
        pipeCamera.enabled = false;
    }

    IEnumerator DisplayCameraHelper()
    {
        yield return new WaitForEndOfFrame();
        pipeCamera.enabled = false;
        yield return new WaitForEndOfFrame();
        pipeCamera.enabled = true;
    }
}
