using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static bool isPassingItem;

    public List<ItemSO> itemsHeld = new();
    public Inventory_UI inv_UI;
    Animator anim;

    public static int potionsDrank = 0;

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

    public void AddItemToInventory(ItemSO itemToAdd)
    {
        itemsHeld.Add(itemToAdd);
        inv_UI.Setup();
    }

    public void DropItem(Slot_UI inventorySlot, ItemSO itemToDrop)
    {
        itemsHeld.Remove(itemToDrop);
        inventorySlot.SetEmpty();
        inv_UI.Setup();
    }

    public void GiveItemToHero(Slot_UI inventorySlot, ItemSO itemToThrow)
    {
        itemsHeld.Remove(itemToThrow);
        inventorySlot.SetEmpty();
        inv_UI.Setup();
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
