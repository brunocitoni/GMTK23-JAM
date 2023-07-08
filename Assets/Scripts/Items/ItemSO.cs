using System.Collections.Generic;
using UnityEngine;

public enum ItemList
{
    Red,
    Grey,
    Brown
}


[CreateAssetMenu(fileName = "ItemDefault", menuName = "ScriptableObjects/Item")]

public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public ItemList itemType;
}



