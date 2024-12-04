using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurePlacer : NeedItemScript
{
    public GameObject lute;
    public Animator doorOpenAnimator;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (doorOpenAnimator == null)
        {
            Debug.LogError("Animator not assigned to FigurePlacer!");
        }
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {

        lute.gameObject.SetActive(true);
        SendMessageUpwards(lute.name + "Done");
        Debug.Log(lute.name + "Done");


        if (doorOpenAnimator != null)
        {
            doorOpenAnimator.SetTrigger("DoorOpen");
            Debug.Log("Animation triggered.");
            audioSource.Play();
        }
        else
        {
            Debug.LogError("doorOpenAnimator is null!");
        }
        

    }
}
