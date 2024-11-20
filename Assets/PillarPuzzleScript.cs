using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPuzzleScript : MonoBehaviour
{
    public float maxPillarY;
    public float minPillarY;
    // Start is called before the first frame update
    void Start()
    {
        BroadcastMessage("SetMaxPillarY", maxPillarY);
        BroadcastMessage("SetMinPillarY", minPillarY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
