using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPlacer : NeedItemScript
{

    public GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Open()
    {
        key.gameObject.SetActive(true);
        SendMessageUpwards(key.name + "Done");
        Debug.Log(key.name + "Done");

        
    }
}
