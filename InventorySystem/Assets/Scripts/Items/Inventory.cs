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

    private int _inventoryCapacity;
    private InventorySlot[] _inventorySlots;
    private List<Item> _items = new List<Item>();

    private void Start()
    {
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();
        _inventoryCapacity = _inventorySlots.Length;
    }

    private void _updateInventorySlots()
    {
        for(int i = 0; i < _items.Count; i++)
        {
            _inventorySlots[i].ClearSlot();
            _inventorySlots[i].AddItem(_items[i]);
        }
    }

    public bool AddItem(Item newItem)
    {
        if(_items.Count < _inventoryCapacity)
        {
            _items.Add(newItem);
            _updateInventorySlots();

            Debug.Log(newItem.name + " picked up.");

            return true;
        }

        Debug.Log("Can't pick up " + newItem.name + ". No inventory space.");
        return false;
    }

    public void RemoveItem(Item removedItem)
    {
        _items.Remove(removedItem);

        _updateInventorySlots();
    }
}
