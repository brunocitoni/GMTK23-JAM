using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefault", menuName = "ScriptableObjects/Item")]

public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
}
