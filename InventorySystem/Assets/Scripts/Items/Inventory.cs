using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            GameObject.Destroy(this);
            return;
        }

        Instance = this;
    }

    #endregion

    public List<Item> _items;

    public void AddItem(Item newItem)
    {
        _items.Add(newItem);
    }

    public void RemoveItem(Item removedItem)
    {
        _items.Remove(removedItem);
    }
}
