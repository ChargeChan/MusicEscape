using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PianoButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int note;
    public string rune;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //Click note
    public void OnPointerDown(PointerEventData eventData)
    {
        SendMessageUpwards("PlayNote", note);
        if (rune != "")
        {
            SendMessageUpwards("PlayRune", rune);
        }
    }
    // mouse up
    public void OnPointerUp(PointerEventData eventData)
    {
        SendMessageUpwards("StopNote");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
