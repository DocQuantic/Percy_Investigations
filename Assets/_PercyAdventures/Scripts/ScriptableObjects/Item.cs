using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public string itemName = "New Item";
    public Sprite icon;
    public bool isPickable;

}
