using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCanvasScript : MonoBehaviour
{
    public GameObject[] bones;
    [SerializeField] private bool allCorrect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckBones()
    {
        allCorrect = true;
        for(int i = 0; i < bones.Length; i++)
        {
            bones[i].SendMessage("SendBoneStatus");
        }
        Debug.Log(allCorrect);
    }

    public void GetBoneStatus(bool isCorrect)
    {
        if(!isCorrect)
        {
            allCorrect=false;
        }
    }

    public void Solved()
    {
        //do whatever for when the pzzle is done
    }
}
