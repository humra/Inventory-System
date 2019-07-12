using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item _item;
    private Image _icon;
    private Text _stackText;
    private int _stackCount = 0;

    private void Start()
    {
        _icon = transform.Find("ItemButton/Image").GetComponent<Image>();
        _stackText = transform.Find("ItemButton/StackCount").GetComponent<Text>();
        _stackText.text = "";
        _icon.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        _item = newItem;
        _icon.sprite = newItem.Icon;
        _icon.enabled = true;
        _stackCount++;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _stackCount = 0;
        _stackText.text = "";
    }

    public void AddOneItem()
    {
        _stackCount++;

        if(_stackCount >=2)
        {
            _stackText.text = _stackCount.ToString();
        }
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
}
