using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedItemScript : MonoBehaviour
{
    public string itemNeeded;
    private bool locked = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GiveItem(string item)
    {
        if(locked && item == itemNeeded)
        {
            locked = false;
            Open();
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Open()
    {
        //gameObject.GetComponentInChildren<Renderer>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}
