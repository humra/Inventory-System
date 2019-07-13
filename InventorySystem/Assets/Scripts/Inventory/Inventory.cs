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
    private Item _temporaryItem;
    private int _temporaryStackCount;
    private int _originIndex;

    private void Start()
    {
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();
        _inventoryCapacity = _inventorySlots.Length;
    }

    public bool AddItem(Item newItem)
    {
        for(int i = 0; i < _inventorySlots.Length; i++)
        {
            if(_inventorySlots[i].GetItem() == newItem)
            {
                if(_inventorySlots[i].AddOneItem())
                {
                    Debug.Log(newItem.name + " added to existing stack.");
                    _inventorySlots[i].UpdateInventorySlot();

                    return true;
                }
                else
                {
                    continue;
                }
            }
        }

        int filledInventorySpaceCounter = 0;

        for(int i = 0; i < _inventorySlots.Length; i++)
        {
            if(_inventorySlots[i].GetItem() != null)
            {
                filledInventorySpaceCounter++;
            }
        }

        if(filledInventorySpaceCounter < _inventoryCapacity)
        {
            _inventorySlots[filledInventorySpaceCounter].SetItem(newItem);
            _inventorySlots[filledInventorySpaceCounter].UpdateInventorySlot();

            Debug.Log(newItem.name + " picked up.");

            return true;
        }

        Debug.Log("Can't pick up " + newItem.name + ".");
        return false;
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
