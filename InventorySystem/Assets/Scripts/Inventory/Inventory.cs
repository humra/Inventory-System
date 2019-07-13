using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private int _destinationIndex;
    private ItemHover _itemHover;

    public IInventoryInteractionHandler InventoryInteractionHandler;

    private void Start()
    {
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();
        _inventoryCapacity = _inventorySlots.Length;
        _itemHover = GameObject.FindObjectOfType<ItemHover>();
        _itemHover.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _temporaryItem != null)
        {
            DropItem(_inventorySlots[_originIndex], _temporaryItem);
            _stopFollowingCursor();
            ClearTemporaryItem();
        }
    }

    private void _startFollowingCursor()
    {
        _itemHover.gameObject.SetActive(true);
        _itemHover._image.sprite = _temporaryItem.Icon;
        _itemHover._text.text = _temporaryStackCount.ToString();
    }

    private void _stopFollowingCursor()
    {
        _itemHover.gameObject.SetActive(false);
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
        InventoryInteractionHandler.ShowInfoMessage("Can't pick up " + newItem.Name + ".");
        return false;
    }

    public void SetTemporaryItemData(InventorySlot inventorySlot)
    {
        _temporaryItem = inventorySlot.GetItem();
        _temporaryStackCount = inventorySlot.GetStackCount();
        _originIndex = GetIndexOfInventorySlot(inventorySlot);

        _startFollowingCursor();

        inventorySlot.ClearSlot();
    }

    public void SwapItemToDestination(InventorySlot inventorySlot)
    {
        inventorySlot.SetItem(_temporaryItem);
        inventorySlot.SetStackCount(_temporaryStackCount);
        _destinationIndex = GetIndexOfInventorySlot(inventorySlot);

        if(_originIndex != _destinationIndex)
        {
            _inventorySlots[_originIndex].ClearSlot();
        }

        _stopFollowingCursor();
        ClearTemporaryItem();
    }

    public void SwapItems(InventorySlot inventorySlot)
    {
        Item tempItem = inventorySlot.GetItem();
        int tempStackCount = inventorySlot.GetStackCount();
        _destinationIndex = GetIndexOfInventorySlot(inventorySlot);

        SwapItemToDestination(inventorySlot);

        _inventorySlots[_originIndex].SetItem(tempItem);
        _inventorySlots[_originIndex].SetStackCount(tempStackCount);
    }

    public void CancelItemSwap()
    {
        _inventorySlots[_originIndex].SetItem(_temporaryItem);
        _inventorySlots[_originIndex].SetStackCount(_temporaryStackCount);
        ClearTemporaryItem();
        _stopFollowingCursor();
    }

    public void DropItem(InventorySlot inventorySlot)
    {
        InventoryInteractionHandler.DropItem(inventorySlot.GetItem());
        _inventorySlots[GetIndexOfInventorySlot(inventorySlot)].ClearSlot();
    }

    public void DropItem(InventorySlot inventorySlot, Item item)
    {
        InventoryInteractionHandler.DropItem(item);
        _inventorySlots[GetIndexOfInventorySlot(inventorySlot)].ClearSlot();
    }

    public bool TemporaryItemExists()
    {
        return _temporaryItem != null;
    }

    public void ClearTemporaryItem()
    {
        _temporaryItem = null;
        _temporaryStackCount = -1;
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
