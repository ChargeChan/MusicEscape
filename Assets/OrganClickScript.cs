using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganClickScript : NeedItemScript
{
    public GameObject hiddenKeys;
    private Collider myCollider;
    // Start is called before the first frame update
    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }
    public override void Open()
    {
        hiddenKeys.SetActive(true);
        Destroy(myCollider);
        Destroy(this);
    }

}
