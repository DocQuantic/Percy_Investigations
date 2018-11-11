using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    private InventoryManager inventory;
    private InventorySlot[] slots;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        inventory = InventoryManager.instance;
        inventory.OnItemAddCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    private void UpdateUI()
    {
        for(int i = 0; i <= slots.Length; i++)
        {
            if (i<inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
        }
    }

}
