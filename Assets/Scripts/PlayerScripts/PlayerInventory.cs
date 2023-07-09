using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    public static bool isPassingItem;

    public List<ItemSO> itemsHeld = new();
    public Inventory_UI inv_UI;
    Animator anim;

    public static int potionsDrank = 0;

    public static int itemsCrafted = 0;

    public CraftingTable craft;
    public CraftingPanel_UI craftingPanelUI;

    public delegate void HealthPotion();
    public static event HealthPotion OnHealthPotionGiven;

    public delegate void AttackPotion();
    public static event AttackPotion OnAttackPotionGiven;

    public delegate void DefencePotion();
    public static event DefencePotion OnDefencePotionGiven;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isPassingItem = false;
    }

    public void RefreshUI() {
        inv_UI.Setup();
        craftingPanelUI.Setup();
    }

    public void AddItemToInventory(ItemSO itemToAdd)
    {
        itemsHeld.Add(itemToAdd);
        RefreshUI();
    }

    public void DropItem(Slot_UI inventorySlot, ItemSO itemToDrop)
    {
        itemsHeld.Remove(itemToDrop);
        inventorySlot.SetEmpty();
        RefreshUI();
    }

    public void GiveItemToHero(Slot_UI inventorySlot, ItemSO itemToThrow)
    {
        itemsHeld.Remove(itemToThrow);
        inventorySlot.SetEmpty();
        RefreshUI();
        ApplyEffect(itemToThrow);
        StartCoroutine(AnimateGivingItem());
    }

    IEnumerator AnimateGivingItem()
    {
        isPassingItem = true;
        anim.SetTrigger("PassItem");
        yield return new WaitForSeconds(0.5f);
        isPassingItem = false;
    }

    public void CraftItem(ItemSO item, List<ItemSO> listRecipe)
    {
       foreach (ItemSO ingredient in listRecipe)
       {
            itemsHeld.Remove(ingredient);
       }
       AddItemToInventory(item);
        itemsCrafted++;


    }

    public List<RecipeSO> craftableItems(List<ItemSO> itemsHeld)
    {
        List<RecipeSO>craftable = new();
        foreach(RecipeSO recipe in craft.recipList)
        {
            bool iscraftable = true;
            List<ItemSO> itemsreallyHeld = new List<ItemSO>(itemsHeld);
            //check if craftable
            List<ItemSO> ingredients = recipe.requiredIngredients;

            foreach (ItemSO item in ingredients)
            {
                if (itemsreallyHeld.Contains(item))
                {
                    itemsreallyHeld.Remove(item);
                }
                else
                {
                    iscraftable = false;
                }
            }
            if(iscraftable){
                craftable.Add(recipe);
            }
            else{
                Debug.Log("Cant craft"+recipe.recipeName);
            }
        }
        return craftable;
    }

    public void ApplyEffect(ItemSO itemGiven)
    {
        Debug.Log("Hero received item " + itemGiven.itemName);
        switch (itemGiven.itemName)
        {
            
            case "HealthPotion":
                // health 1/2 hp to hero
                Debug.Log("health potion given, invoking now");
                potionsDrank++;
                OnHealthPotionGiven?.Invoke();
                break;
            case "AttackPotion":
                // add some attack power for some time
                Debug.Log("attack potion given, invoking now");
                potionsDrank++;
                OnAttackPotionGiven?.Invoke();
                break;
            case "DefencePotion":
                //add some shild for some time
                Debug.Log("defence potion given, invoking now");
                potionsDrank++;
                OnDefencePotionGiven?.Invoke();
                    break;
            default:
                break;

        }

    }
}
