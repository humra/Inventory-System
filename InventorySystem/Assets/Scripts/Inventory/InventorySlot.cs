using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item _item;
    private Image _icon;
    private Text _stackText;
    private int _stackCount = 0;
    private ItemButton _itemButton;

    private void Start()
    {
        _icon = transform.Find("ItemButton/ContentParent/Image").GetComponent<Image>();
        _stackText = transform.Find("ItemButton/ContentParent/StackCount").GetComponent<Text>();
        _stackText.text = "";
        _icon.enabled = false;
        _itemButton = GetComponentInChildren<ItemButton>();
    }

    public void AddItem(Item newItem)
    {
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

        _itemButton.HasItem = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _stackCount = 0;
        _stackText.text = "";
        _itemButton.HasItem = false;
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

    public void UpdateInventorySlot()
    {
        _icon.sprite = _item.Icon;
        if (_stackCount >= 2)
        {
            _stackText.text = _stackCount.ToString();
        }
    }
}
