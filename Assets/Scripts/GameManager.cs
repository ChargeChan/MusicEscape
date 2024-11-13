using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject inventoryBar;
    private static GameManager _instance;
    [SerializeField] private string[] inventory;
    private int currentCameraIndex = 0;

    private GameManager()
    {
        inventory = new string[5];
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<GameManager>();
                _instance = go.GetComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        inventoryBar = GameObject.Find("InventoryBar");
    }


    public void AddItemToIneventory(string item)
    {
        if(item == "PanFlute")
        {
            inventoryBar.SendMessage("EnablePanFlute");
            return;
        }
        for(int i=0; i<inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                inventoryBar.SendMessage("SetInventory", inventory);
                return;
            }
        }
        Debug.Log("Inventory Full");
    }

    public void RemoveItemFromInventory(int position)
    {
        inventory[position] = null;
        inventoryBar.SendMessage("SetInventory", inventory);
    }

    public void SetCurrentCameraIndex(int index)
    {
        currentCameraIndex = index;
    }

    public int GetCurrentCameraIndex()
    {
        return currentCameraIndex;
    }
}
