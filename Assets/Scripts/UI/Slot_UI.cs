using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;
    public ItemSO thisItem;
    PlayerInventory plInv;
    public bool nearHero;

    public Transform heroTransform;
    public Transform playerTransform;
    public float throwingDistance = 2f;

    private void Start()
    {
        plInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        heroTransform = GameObject.FindGameObjectWithTag("Hero").transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetItem(ItemSO item)
    {
        thisItem = item;

        if (item != null)
        {
            itemIcon.sprite = thisItem.itemSprite;
            itemIcon.color = new Color(1, 1, 1, 1);
        }
    }

    public void SetEmpty()
    {
        thisItem = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (thisItem == null)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (nearHero && thisItem.givable)
            {
                // give item
                Debug.Log("Left click");
                plInv.GiveItemToHero(this, thisItem);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // drop item
            Debug.Log("Right click");
            plInv.DropItem(this, thisItem);
        }
    }

    private void Update()
    {
        if (thisItem == null) {
            return;
        }

        float distance = Vector3.Distance(heroTransform.position, playerTransform.position);

        if (distance < throwingDistance)
        {
            nearHero = true;

            if (thisItem.givable)
            {
                // tint green for throwable
                itemIcon.color = Color.green;
            }
        }
        else
        {
            nearHero = false;
            itemIcon.color = new Color(1, 1, 1, 1);
        }
    }

}