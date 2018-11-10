using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public delegate void onItemAdd();
    public onItemAdd OnItemAddCallback;

    #region Singleton;

    public static InventoryManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion;

    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        OnItemAddCallback.Invoke();
    }

}
