using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuillPlacerScript : NeedItemScript
{
    public GameObject quill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Open()
    {
        
        quill.gameObject.SetActive(true);
        SendMessageUpwards(quill.name + "Done");
        Debug.Log(quill.name + "Done");
    }
}
