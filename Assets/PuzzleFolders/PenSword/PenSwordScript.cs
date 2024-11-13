using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenSwordScript : MonoBehaviour
{
    public GameObject quillHolder;
    public GameObject swordHolder;
    public GameObject solutionObject;
    public Animation solutionAnimation;
    public Animator animator;
    private bool swordDone = false;
    private bool quillDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordDone()
    {
        swordDone = true;
        if(quillDone)
            CompletePuzzle();
    }

    public void QuillDone()
    {
        quillDone = true;
        if (swordDone)
            CompletePuzzle();
    }

    private void CompletePuzzle()
    {
        //solutionObject.SendMessage("PuzzleComplete");
        //solutionAnimation.Play("OpenDoor");
        animator.Play("PenSwordDoorAnim");
    }
}
