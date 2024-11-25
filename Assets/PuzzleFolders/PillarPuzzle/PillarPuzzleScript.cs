using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPuzzleScript : MonoBehaviour
{
    public float maxPillarY;
    public float minPillarY;
    private bool correctPillar1 = false;
    private bool correctPillar2 = false;
    private bool correctPillar3 = false;
    private bool correctPillar4 = false;


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

    public void CorrectHeightOn(int note)
    {
        switch(note)
        {
            case 65:
                correctPillar1 = true; break;
            case 70:
                correctPillar2 = true; break;
            case 72:
                correctPillar3 = true; break;
            case 77:
                correctPillar4 = true; break;
            default:
                break;
        }
        if(correctPillar1 && correctPillar2 && correctPillar3 && correctPillar4)
        {
            BroadcastMessage("Solved");
        }
    }

    public void CorrectHeightOff(int note)
    {
        switch (note)
        {
            case 65:
                correctPillar1 = false; break;
            case 70:
                correctPillar2 = false; break;
            case 72:
                correctPillar3 = false; break;
            case 77:
                correctPillar4 = false; break;
            default:
                break;
        }
    }
}
