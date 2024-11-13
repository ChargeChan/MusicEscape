using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickCanvasScript : MonoBehaviour
{
    public Canvas myCanvas;
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        myCanvas.enabled = true;
    }
}
