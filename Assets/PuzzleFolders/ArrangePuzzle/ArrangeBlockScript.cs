using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrangeBlockScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool onCursor = false;
    private int index;
    public TextMeshProUGUI indexText;
    public int note;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onCursor)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onCursor = true;
        SendMessageUpwards("BlockDrag", index);
        transform.SetAsLastSibling();
    }
    // mouse up
    public void OnPointerUp(PointerEventData eventData)
    {
        onCursor= false;
        SendMessageUpwards("BlockDrop");
    }

    public void SetIndex(int index)
    {
        this.index = index;
        indexText.text = index.ToString();
    }

    public void SendNoteUp()
    {
        SendMessageUpwards("EnterNote", note);
    }
}
