using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : EventTrigger
{
    private Item _item;
    private Image _icon;
    private Text _stackText;
    private int _stackCount = 0;

    private void Start()
    {
        _icon = transform.Find("ContentParent/Image").GetComponent<Image>();
        _stackText = transform.Find("ContentParent/StackCount").GetComponent<Text>();
        _stackText.text = "";
        _icon.enabled = false;
    }

    public void SetItem(Item newItem)
    {
        if(newItem == null)
        {
            ClearSlot();
        }

        _item = newItem;
        _icon.sprite = newItem.Icon;
        _icon.enabled = true;

        if(_stackCount == 0)
        {
            _stackCount++;
        }
        else if (_stackCount >= 2)
        {
            _stackText.text = _stackCount.ToString();
        }
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _stackCount = 0;
        _stackText.text = "";
    }

    public bool AddOneItem()
    {
        if(_item.GetType() == typeof(NonEquippableItem))
        {
            NonEquippableItem tempCast = (NonEquippableItem)_item;
            if(_stackCount + 1 > tempCast.MaximumStack)
            {
                return false;
            }

            _stackCount++;

            if (_stackCount >= 2)
            {
                _stackText.text = _stackCount.ToString();
            }

            return true;
        }

        return false;
    }

    public void RemoveOneItem()
    {
        _stackCount--;

        if(_stackCount <= 0)
        {
            ClearSlot();
        }
        else if(_stackCount >= 2)
        {
            _stackText.text = _stackCount.ToString();
        }
    }

    public int GetStackCount()
    {
        return _stackCount;
    }

    public void SetStackCount(int stackCount)
    {
        _stackCount = stackCount;
    }

    public void UpdateInventorySlot()
    {
        _icon.sprite = _item.Icon;
        if (_stackCount >= 2)
        {
            _stackText.text = _stackCount.ToString();
        }
    }

    public Item GetItem()
    {
        return _item;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(_item != null)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    Debug.Log("Left click");
                    Inventory.Instance.SetTemporaryItemData(this);
                    break;
                case PointerEventData.InputButton.Right:
                    Debug.Log("Right click");
                    break;
                case PointerEventData.InputButton.Middle:
                    Debug.Log("Middle click");
                    break;
            }
        }
        else
        {
            if(eventData.button == PointerEventData.InputButton.Left && Inventory.Instance.TemporaryItemExists())
            {
                Inventory.Instance.SwapItemToDestination(this);
            }
        }
    }
}
