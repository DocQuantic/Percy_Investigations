using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;

    private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.enabled = true;
        icon.sprite = item.icon;
    }

}
