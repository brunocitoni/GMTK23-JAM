using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{

    public bool canCraft;
    public List<RecipeSO> recipList;

    // Start is called before the first frame update
    void Start()
    {
        canCraft = false;
        WaveManager.OnWaveComplete += ToggleCraftingOff;
    }

    private void OnDestroy()
    {
        WaveManager.OnWaveComplete -= ToggleCraftingOff;
    }

    // Update is called once per frame
    void ToggleCraftingOff()
    {
        canCraft = false;
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            canCraft  =true;

        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            canCraft  =false;

        }
    }
}
