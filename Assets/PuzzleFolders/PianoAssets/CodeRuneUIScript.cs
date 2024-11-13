using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeRuneUIScript : MonoBehaviour
{
    [SerializeField] private string rune;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRune(string rune)
    {
        this.rune = rune;
        if (rune == "")
        {
            image.material = null;
            image.enabled = false;
        }
        else
        {
            Material mat = Resources.Load<Material>(rune);
            image.material = mat;
            image.enabled = true;
        }
    }

    public void ColorIndicate(string state)
    {
        if(state == "wrong")
        {
            image.color = Color.red;
        }
        else if(state == "right")
        {
            image.color = Color.green;
        }
        else if (state == "none")
        {
            image.color = Color.white;
        }
    }
}
