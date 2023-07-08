using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public ItemSO itemSo;
    PlayerManager pM;
    PlayerInventory pI;
    // Start is called before the first frame update
    void Start()
    {
        pM = GetComponent<PlayerManager>();
        pI = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Item")
        {
            ItemSO item = coll.GetComponent<Item>().thisItem;
            if (item != null)
            {
                pI.AddItemToInventory(item);
                Destroy(coll.gameObject);
            }

        }
    }

}
