using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemSO thisItem;
    // Start is called before the first frame update
    void Start()
    {
        name = thisItem.itemName;
        this.GetComponent<SpriteRenderer>().sprite = thisItem.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}







