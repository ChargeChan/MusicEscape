using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class keyPlacer : NeedItemScript
{

    //public GameObject key;
    public GameObject chestBlocker;
    public Animator chestOpenAnimator;

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
    }

    public override void Open()
    {
        //if (key != null)
        //{
        //    key.gameObject.SetActive(true);
        //    Debug.Log("Key activated.");
        //}

        //SendMessageUpwards(key.name + "Done");
        //Debug.Log(key.name + "Done");

        if (chestOpenAnimator != null)
        {
            chestOpenAnimator.SetTrigger("ChestHasKey");
            Debug.Log("Animation triggered.");
        }
        else
        {
            Debug.LogError("chestOpenAnimator is null!");
        }
        chestBlocker.SetActive(false);
    }
}
