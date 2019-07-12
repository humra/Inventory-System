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

    public IInventoryHandler InventorySlotHandler;

    private void Start()
    {
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();
        _inventoryCapacity = _inventorySlots.Length;
    }

    private void _updateInventoryUI()
    {
        InventorySlotHandler.UpdateInventoryUI();
    }

    private void _updateInventorySlots()
    {
        for(int i = 0; i < _items.Count; i++)
        {
            _inventorySlots[i].ClearSlot();
            _inventorySlots[i].AddItem(_items[i]);
        }
    }

    public void AddItem(Item newItem)
    {
        if(_items.Count < _inventoryCapacity)
        {
            _items.Add(newItem);
        }

        _updateInventorySlots();
        _updateInventoryUI();
    }

    public void RemoveItem(Item removedItem)
    {
        _items.Remove(removedItem);

        _updateInventorySlots();
        _updateInventoryUI();
    }
}
