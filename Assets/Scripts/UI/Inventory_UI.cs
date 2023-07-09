using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject player;
    PlayerInventory pInventory;
    public GameObject cTable;
    CraftingTable cTableScript;
    public List<Slot_UI> slots = new List<Slot_UI>();
    //public Sprite icon;

    void Start()
    {
        pInventory = player.GetComponent<PlayerInventory>();
        cTableScript = cTable.GetComponent<CraftingTable>();
        Setup();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();    
        }
    }

    public void ToggleInventory()
    {
        Setup();
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
        }
        else
        {
            /*if(!cTableScript.canCraft){
                inventoryPanel.SetActive(false);
            }*/
            inventoryPanel.SetActive(false);
        }
    }

    public void Setup()
    {   
        List<ItemSO> inventory = pInventory.itemsHeld;
        for(int i=0; i<slots.Count; i++)
        {
            if(i<inventory.Count)
            {
                slots[i].SetItem(inventory[i]);
            }
            else
            {
                slots[i].SetEmpty();
            }
        }
    }
}
