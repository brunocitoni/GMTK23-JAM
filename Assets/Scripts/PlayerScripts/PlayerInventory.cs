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

    public void ThrowItem(ItemSO itemToThrow)
    {
        itemsHeld.Remove(itemToThrow);
        // todo hero needs to receive it
    }
}
