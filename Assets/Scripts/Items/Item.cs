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
        this.GetComponent<Image>().sprite = thisItem.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}







