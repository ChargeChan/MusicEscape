using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpenScript : MonoBehaviour
{
    private bool isOpen;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(isOpen)
        {
            isOpen = false;
            animator.SetTrigger("DrawerClose");
        }
        else
        {
            isOpen = true;
            animator.SetTrigger("DrawerOpen");
        }
        
    }
}
