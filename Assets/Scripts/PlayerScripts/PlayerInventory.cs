using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSO> itemsHeld = new();
    public Inventory_UI inv_UI;

    public void AddItemToInventory(ItemSO itemToAdd)
    {
        itemsHeld.Add(itemToAdd);
        inv_UI.Setup();
    }

    public void DropItem(Slot_UI inventorySlot, ItemSO itemToDrop)
    {
        itemsHeld.Remove(itemToDrop);
        inventorySlot.SetEmpty();
        inv_UI.Setup();

    }

    public void GiveItemToHero(Slot_UI inventorySlot, ItemSO itemToThrow)
    {
        itemsHeld.Remove(itemToThrow);
        inventorySlot.SetEmpty();
        inv_UI.Setup();
        ApplyEffect(itemToThrow);
        // todo hero needs to receive it
    }

    public void ApplyEffect(ItemSO itemGiven)
    {
        //todo
        switch (itemGiven.itemName)
        {

            case "HealthPotion":
                break;

        }

    }
}
