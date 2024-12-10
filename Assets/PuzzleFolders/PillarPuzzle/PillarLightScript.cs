using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarLightScript : MonoBehaviour
{
    private Renderer myRenderer;
    public int note;

    private float intensity = 0;
    private bool isOn = true;
    public Color myColor;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.SetColor("myColor", myColor);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOn)
        {
            //myRenderer.material.color = color * intensity;
            //Color tempCol = myColor;
            //tempCol.a = intensity;
            //myRenderer.material.SetColor("test_name", tempCol);
            
            intensity = intensity - 0.01f;
            myColor.a = intensity;
            myRenderer.material.color = myColor;
            
            if(intensity < 0f)
            {
                intensity = 0f;
                myColor.a = intensity;
                myRenderer.material.color = myColor;
                isOn = false;
            }
                
        }
    }

    public void PlayNote(int note)
    {
        if (note == this.note)
        {
            myColor.a = intensity = 1.0f;
            myRenderer.material.color = myColor;
            isOn = false;
        }
    }

    public void TurnOff()
    {
        isOn = true;
    }
}
