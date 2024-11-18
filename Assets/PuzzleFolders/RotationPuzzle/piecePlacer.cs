using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piecePlacer : NeedItemScript
{
    public GameObject harpPiece;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Open()
    {

        harpPiece.gameObject.SetActive(true);
        SendMessageUpwards(harpPiece.name + "Done");
        Debug.Log(harpPiece.name + "Done");
    }
}
