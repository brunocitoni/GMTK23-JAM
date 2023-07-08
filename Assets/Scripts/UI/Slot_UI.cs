using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    public Image itemIcon;
    
    public void SetItem(ItemSO item)
    {
        if(item != null)
        {
            itemIcon.sprite = item.itemSprite;
            itemIcon.color = new Color(1,1,1,1);
        }
    }

    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1,1,1,0);
    }
}
