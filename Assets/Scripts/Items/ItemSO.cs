using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDefault", menuName = "ScriptableObjects/Item")]

public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public bool givable = false;
    public bool craftable = false;
    public string description;
}



