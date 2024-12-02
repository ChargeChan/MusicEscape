using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurePlacer : NeedItemScript
{
    public GameObject lute;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Open()
    {

        lute.gameObject.SetActive(true);
        SendMessageUpwards(lute.name + "Done");
        Debug.Log(lute.name + "Done");
    }
}
