using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarLightScript : MonoBehaviour
{
    private Renderer myRenderer;
    public int note;

    private float intensity = -15f;
    private bool isOn;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.color = color * -15f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            myRenderer.material.color = color * intensity;
            intensity = intensity - 0.005f;
            if(intensity < -15f)
            {
                
                isOn = false;
            }
                
        }
    }

    public void PlayNote(int note)
    {
        if (note == this.note)
        {
            intensity = 1f;
            myRenderer.material.color = color;
            isOn = false;
        }
    }

    public void TurnOff()
    {
        isOn = true;
    }
}
