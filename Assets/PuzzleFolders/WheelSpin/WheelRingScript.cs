using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WheelRingScript : MonoBehaviour
{
    public int note;
    public float initialRotation;
    private float rotationSpeed = 0.1f;
    private bool isRotating;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(0, initialRotation, 0, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.localRotation.eulerAngles.y);
        if (isRotating)
        {
            gameObject.transform.Rotate(0, rotationSpeed, 0, Space.Self);
        }
        else
        {
            if(gameObject.transform.localRotation.eulerAngles.y % 30 > 0.1)
            {
                //Debug.Log(gameObject.transform.localRotation.eulerAngles.y % 30);
                gameObject.transform.Rotate(0, rotationSpeed, 0);
            }
            else
            {

            }
        }
    }

    public void PlayNote(int note)
    {
        if (note == this.note)
        {
            isRotating = true;
        }
    }

    public void StopNote()
    {
        isRotating=false;
    }
}
