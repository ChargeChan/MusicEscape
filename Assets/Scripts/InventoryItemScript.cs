using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    Image myImage;
    public int myIndex;
    private string myName;
    private bool onCursor;
    private Camera camera;
    void Start()
    {
        myImage = GetComponent<Image>();
        myImage.enabled = false;
        onCursor = false;
        camera = Camera.main;
    }


    public void SetImage(string imageName)
    {
        Material mat = Resources.Load<Material>(imageName);
        myImage.material = mat;
        myImage.enabled = true;
        if(imageName == "null")
            myImage.enabled = false;
        myName = imageName;
    }

    public void TestGameManager(int position)
    {
        //GameManager.Instance.SendMessage("RemoveItemFromInventory", position);
    }

    public void ClickItem()
    {
        camera = Camera.main;

        if (!onCursor)
        {
            onCursor = true;
        }
        else
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                //Debug.Log(objectHit.gameObject);
                //Debug.DrawRay(ray.origin, ray.direction * 3, color:Color.green, 4);
                
                NeedItemScript needItem;
                if(objectHit.gameObject.TryGetComponent<NeedItemScript>(out NeedItemScript myItem))
                {
                    needItem = myItem;
                    if(myItem.GiveItem(myName))
                    {
                        //Debug.Log("item taken");
                        GameManager.Instance.RemoveItemFromInventory(myIndex);
                    }
                    else
                    {
                        //Debug.Log("item not taken");
                    }
                }
                else
                {
                    //Debug.Log("get component fail");
                }

                
            }

            onCursor = false;
            transform.position = gameObject.transform.parent.position;
        }
    }

    public void Update()
    {
        if(onCursor)
        {
            transform.position = Input.mousePosition;
        }
    }

    
}
