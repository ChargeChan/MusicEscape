using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skull : MonoBehaviour
{
    public delegate void SkullClicked(int skullID);
    public event SkullClicked OnSkullClicked;

    public int note;
    private int skullID; // Unique ID for this skull

    /*public Material glowMaterial;*/ // Optional: Assign a glowing material for effects
    private Material originalMaterial;
    private Renderer skullRenderer;

    void Start()
    {
        skullRenderer = GetComponent<Renderer>();
        if (skullRenderer != null)
        {
            originalMaterial = skullRenderer.material;
        }
    }

    public void AssignID(int id)
    {
        skullID = id;
    }

    void OnMouseDown()
    {
        // Notify the manager that this skull was clicked
        OnSkullClicked?.Invoke(skullID);
        SendMessageUpwards("PlayNote", note);

        // Trigger glow effect
        //if (skullRenderer != null && glowMaterial != null)
        //{
        //    skullRenderer.material = glowMaterial;
        //}
    }

    
    

    public void ResetVisuals()
    {
        // Reset to the original material
        if (skullRenderer != null && originalMaterial != null)
        {
            skullRenderer.material = originalMaterial;
            
        }
    }
}