using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class keyPlacer : NeedItemScript
{

    //public GameObject key;
    public GameObject chestBlocker;
    public Animator chestOpenAnimator;
    public AudioSource audioSource;

    void Start()
    {
        //if (key != null)
        //{
        //    key.SetActive(false); // Ensure the key is hidden initially
        //}

        if (chestOpenAnimator == null)
        {
            Debug.LogError("Animator not assigned to keyPlacer!");
        }
        audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        

        if (chestOpenAnimator != null)
        {
            chestOpenAnimator.SetTrigger("ChestHasKey");
            Debug.Log("Animation triggered.");
            audioSource.Play();
        }
        else
        {
            Debug.LogError("chestOpenAnimator is null!");
        }
        chestBlocker.SetActive(false);
    }
}
