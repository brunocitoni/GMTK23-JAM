using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemSO scriptableObject;
    [SerializeField] GameObject itemPrefab;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void InstanciateScriptableObject()
    {
       
        var newItem = Instantiate(itemPrefab, this.transform);
        newItem.transform.position = this.transform.position;
        newItem.GetComponent<Item>().thisItem = scriptableObject; // assign a random enemy to this specific enemy
    }

    // Update is called once per frame
    public void InstantiateItem(ItemSO itemToInstantiate, Transform spawnTransform)
    {
        var newItem = Instantiate(itemPrefab, this.transform);
        newItem.transform.position = spawnTransform.position;
        newItem.GetComponent<Item>().thisItem = itemToInstantiate; // assign a random enemy to this specific enemy
    }
}






