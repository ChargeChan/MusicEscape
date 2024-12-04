using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChordLightScript : MonoBehaviour
{
    private Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        //myRenderer.material.color = Color.green;
        myImage.color = Color.green;
    }

    public void TurnOff()
    {
        myImage.color = Color.white;
    }

    public void ColorSetting()
    {
        myImage.color = Color.red;
    }
}
