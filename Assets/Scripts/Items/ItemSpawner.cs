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
        
        InstanciateScriptableObject();

    }

    // Update is called once per frame
    private void InstanciateScriptableObject()
    {
        float x = Random.Range (-31, 31);
        float y = Random.Range (-14, 14);
        Vector3 pos1 = new Vector3 (x, y, 0);
        
        var newItem = Instantiate(itemPrefab, this.transform);
        newItem.transform.position = pos1;
        newItem.GetComponent<Item>().thisItem = scriptableObject; // assign a random enemy to this specific enemy
    }

    // Update is called once per frame
    public void InstantiateItem(ItemSO itemToInstantiate, Transform spawnTransform)
    {
        var newItem = Instantiate(itemPrefab, spawnTransform);
        newItem.transform.position = spawnTransform.position;
        newItem.GetComponent<Item>().thisItem = itemToInstantiate; // assign a random enemy to this specific enemy
    }
}






