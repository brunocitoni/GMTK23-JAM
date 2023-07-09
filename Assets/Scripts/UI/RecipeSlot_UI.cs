using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RecipeSlot_UI : MonoBehaviour
{
    public Image itemIcon;
    public ItemSO thisItem;
    public List<ItemSO> thisListRecipe;
    public Button button;
    public TextMeshProUGUI texteBouton;
    PlayerInventory plInv;
    CraftingPanel_UI cpUI;

    void Start()
    {
        plInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        button.onClick.AddListener(OnClick);
        cpUI = GetComponentInParent<CraftingPanel_UI>();
    }
    public void SetItem(ItemSO item, List<ItemSO> listRecipe)
    {
        thisItem = item;
        thisListRecipe = listRecipe;

        if (item != null)
        {
            itemIcon.sprite = thisItem.itemSprite;
            itemIcon.color = new Color(1, 1, 1, 1);
            button.gameObject.SetActive(true);
            texteBouton.text = thisItem.itemName;
        }
    }

    void OnClick()
    {
        plInv.CraftItem(thisItem,thisListRecipe);
        cpUI.Setup();
    }

    public void SetEmpty()
    {
        thisItem = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        button.gameObject.SetActive(false);
    }

}
