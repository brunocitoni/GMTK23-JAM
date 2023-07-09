using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel_UI : MonoBehaviour
{
    public GameObject craftingPanel;
    public GameObject player;
    public GameObject cTable;
    PlayerInventory pInventory;
    CraftingTable cTableScript;
    public List<RecipeSlot_UI> rslots = new List<RecipeSlot_UI>();
    // Start is called before the first frame update
    void Start()
    {
        pInventory = player.GetComponent<PlayerInventory>();
        cTableScript = cTable.GetComponent<CraftingTable>();
        Setup();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && cTableScript.canCraft)
        {
            ToggleCraftingPanel();    
        }
        if(!cTableScript.canCraft)
        {
            craftingPanel.SetActive(false);    
        }
        //fix alternance si on va sur craft avec inventaire ouvert
    }

    public void ToggleCraftingPanel()
    {
        Setup();
        if (!craftingPanel.activeSelf)
        {
            craftingPanel.SetActive(true);
        }
        else
        {
            craftingPanel.SetActive(false);
        }
    }
        
    public void Setup()
    {   
        List<RecipeSO> craftable = pInventory.craftableItems(pInventory.itemsHeld); 
        Debug.Log(craftable.Count);
        for(int i=0; i<rslots.Count; i++)
        {
            if(i<craftable.Count)
            {
                rslots[i].SetItem(craftable[i].recipeResult,craftable[i].requiredIngredients);
            }
            else
            {
                rslots[i].SetEmpty();
            }
        }
    }
}

