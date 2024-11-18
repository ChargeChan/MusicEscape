using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPuzzleScript : MonoBehaviour
{
    public GameObject[] rings;

    private bool isRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void CheckForSolution()
    {
        bool isCorrect = true;
        for (int i = 0; i < rings.Length; i++)
        {
            if (!(rings[i].gameObject.transform.localRotation.eulerAngles.y <= 0.5f))
            {
                isCorrect = false;
            }
        }
        if (isCorrect)
        {
            Debug.Log("Correct!");
        }
    }
}
