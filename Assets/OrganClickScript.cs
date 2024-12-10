using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganClickScript : NeedItemScript
{
    public GameObject hiddenKeys;
    private Collider myCollider;
    public bool debugCheat;
    // Start is called before the first frame update
    private void Start()
    {
        myCollider = GetComponent<Collider>();
        if (debugCheat)
            Open();
    }
    public override void Open()
    {
        hiddenKeys.SetActive(true);
        Destroy(myCollider);
        Destroy(this);
    }

}
