using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuillPlacerScript : NeedItemScript
{
    public GameObject quill;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        
        quill.gameObject.SetActive(true);
        audioSource.Play();
        SendMessageUpwards(quill.name + "Done");
        Debug.Log(quill.name + "Done");
    }
}
