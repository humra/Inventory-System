using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item _item;
    private Image _icon;

    private void Start()
    {
        _icon = transform.Find("ItemButton/Image").GetComponent<Image>();
        _icon.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        _item = newItem;
        _icon.sprite = newItem.Icon;
        _icon.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }
}
