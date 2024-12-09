using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int note;
    public float upSpeed;
    public float downSpeed;
    public float correctHeight;
    private bool goingUp = false;
    private bool goingDown = false;
    private float maxPillarY;
    private float minPillarY;
    private bool solved = false;
    // Start is called before the first frame update
    void Start()
    {


    }

    private void Update()
    {
        if(!solved)
        {
            if(transform.position.y > correctHeight - 0.2 && transform.position.y < correctHeight + 0.5)
            {
                SendMessageUpwards("CorrectHeightOn", note);
                Debug.Log("Chorrect " + note);
            }
            else
            {
                SendMessageUpwards("CorrectHeightOff", note);
            }
            if (goingUp)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + upSpeed, transform.position.z);
                if (transform.position.y > maxPillarY)
                {
                    goingUp = false;
                    goingDown = false;
                    gameObject.transform.position = new Vector3(transform.position.x, maxPillarY, transform.position.z);
                }
            }
            else if (goingDown)
            {
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
                if (transform.position.y < minPillarY)
                {
                    goingDown = false;
                    gameObject.transform.position = new Vector3(transform.position.x, minPillarY, transform.position.z);
                }
                Debug.Log(transform.position.y);
            }
        }
        else
        {

        }
        
    }

    public void PlayNote(int note)
    {
        if (note == this.note)
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        goingUp = true;
    }

    public void TurnOff()
    {
        goingUp = false;
        goingDown = true;
    }

    public void SetMinPillarY(float y)
    {
        minPillarY = y;
        gameObject.transform.position = new Vector3(transform.position.x, minPillarY, transform.position.z);
    }

    public void SetMaxPillarY(float y)
    {
        maxPillarY = y;
    }

    public void Solved()
    {
        solved = true;
    }
}
