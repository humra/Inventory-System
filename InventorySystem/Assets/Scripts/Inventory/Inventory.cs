﻿using System.Collections.Generic;
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
    private Item _temporaryItem;
    private int _temporaryStackCount;
    private int _originIndex;

    private void Start()
    {
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();
        _inventoryCapacity = _inventorySlots.Length;
    }

    private void _updateInventorySlots()
    {
        for(int i = 0; i < _items.Count; i++)
        {
            _inventorySlots[i].SetItem(_items[i]);
            _inventorySlots[i].UpdateInventorySlot();
        }
    }

    public bool AddItem(Item newItem)
    {
        foreach(Item existingItem in _items)
        {
            if(existingItem == newItem)
            {
                if(_inventorySlots[_items.IndexOf(existingItem)].AddOneItem())
                {
                    Debug.Log(newItem.name + " added to existing stack.");
                    _updateInventorySlots();
                    
                    return true;
                }
                else
                {
                    continue;
                }
            }
        }

        if (_items.Count < _inventoryCapacity)
        {
            _items.Add(newItem);
            _updateInventorySlots();

            Debug.Log(newItem.name + " picked up.");

            return true;
        }

        Debug.Log("Can't pick up " + newItem.name + ".");
        return false;
    }

    public void RemoveItem(Item removedItem)
    {
        _items.Remove(removedItem);
        _updateInventorySlots();
    }

    public void SetTemporaryItemData(InventorySlot inventorySlot)
    {
        _temporaryItem = inventorySlot.GetItem();
        _temporaryStackCount = inventorySlot.GetStackCount();
        _originIndex = GetIndexOfInventorySlot(inventorySlot);
    }

    public void SwapItemToDestination(InventorySlot inventorySlot)
    {
        inventorySlot.SetItem(_temporaryItem);
        inventorySlot.SetStackCount(_temporaryStackCount);
        _inventorySlots[_originIndex].ClearSlot();
        ClearTemporaryItem();
    }

    public bool TemporaryItemExists()
    {
        return _temporaryItem != null;
    }

    public void ClearTemporaryItem()
    {
        _temporaryItem = null;
    }

    public int GetIndexOfInventorySlot(InventorySlot inventorySlot)
    {
        for(int i = 0; i < _inventorySlots.Length; i++)
        {
            if(_inventorySlots[i] == inventorySlot)
            {
                return i;
            }
        }

        return -1;
    }
}
