using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoneScript : MonoBehaviour, IPointerDownHandler
{
    private RawImage rawImage;
    private bool selected;
    public bool shouldBeSelected;
    public Color selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selected)
        {
            selected = false;
            rawImage.color = Color.white;
        }
        else
        {
            selected = true;
            rawImage.color = selectedColor;
        }
    }

    public void SendBoneStatus()
    {
        SendMessageUpwards("GetBoneStatus", selected == shouldBeSelected);
    }
}
