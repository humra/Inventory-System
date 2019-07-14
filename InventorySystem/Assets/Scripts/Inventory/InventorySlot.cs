using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : EventTrigger, IPointerEnterHandler, IPointerExitHandler
{
    private Item _item;
    private Image _icon;
    private Image _panelImage;
    private Color _originalColor;
    private Text _stackText;
    private int _stackCount = 0;

    public IItemHoverHandler ItemHoverHandler;

    private void Start()
    {
        _icon = transform.Find("ContentParent/Image").GetComponent<Image>();
        _stackText = transform.Find("ContentParent/StackCount").GetComponent<Text>();
        _panelImage = transform.Find("ContentParent").GetComponent<Image>();
        _originalColor = _panelImage.color;
        _stackText.text = "";
        _icon.enabled = false;
    }

    private void _updateStackCountText()
    {
        if(_stackCount >= 2)
        {
            _stackText.text = _stackCount.ToString();
        }
        else
        {
            _stackText.text = "";
        }
    }

    public void Highlight()
    {
        _panelImage.color = Color.yellow;
    }

    public void StopHighlight()
    {
        _panelImage.color = _originalColor;
    }

    public void SetItem(Item newItem)
    {
        if(newItem == null)
        {
            ClearSlot();
            return;
        }

        _item = newItem;
        _icon.sprite = newItem.Icon;
        _icon.enabled = true;

        if(_stackCount == 0)
        {
            _stackCount++;
        }

        _updateStackCountText();
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

            _updateStackCountText();

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

        _updateStackCountText();
    }

    public int GetStackCount()
    {
        return _stackCount;
    }

    public void SetStackCount(int stackCount)
    {
        _stackCount = stackCount;

        _updateStackCountText();
    }

    public Item GetItem()
    {
        return _item;
    }

    public void UpdateInventorySlot()
    {
        if(_item == null)
        {
            return;
        }

        _icon.sprite = _item.Icon;
        _updateStackCountText();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(_item != null && !Inventory.Instance.TemporaryItemExists())
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        Inventory.Instance.DropItem(this);
                        break;
                    }
                    Inventory.Instance.SetTemporaryItemData(this);
                    break;

                case PointerEventData.InputButton.Right:
                    Inventory.Instance.EquipItem(this);
                    break;
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(Inventory.Instance.TemporaryItemExists())
            {
                if (_item == null)
                {
                    Inventory.Instance.SwapItemToDestination(this);
                }
                else
                {
                    Inventory.Instance.SwapItems(this);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(_item == null)
        {
            return;
        }

        Highlight();
        ItemHoverHandler.ShowItemInfo(_item);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (_item == null)
        {
            return;
        }

        StopHighlight();
        ItemHoverHandler.StopShowingItemInfo();
    }
}
