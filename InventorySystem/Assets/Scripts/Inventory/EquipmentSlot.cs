using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class EquipmentSlot : UnityEngine.EventSystems.EventTrigger
{
    private Equipment _item;
    private Image _image;
    private Sprite _defaultSprite;

    public EnumEquipmentSlot EquipmentType;
    public IEquipmentHandler EquipmentHandler;

    private void Start()
    {
        _image = GetComponent<Image>();
        _defaultSprite = _image.sprite;
    }

    public void SetItem(Equipment item)
    {
        Debug.Log(item.Name + " equipped to " + EquipmentType.ToString());

        _item = item;
        _image.sprite = _item.Icon;

        PlayerAttributes.Strength += _item.StrModifier;
        PlayerAttributes.Dexterity += _item.DexModifier;
        PlayerAttributes.Constitution += _item.ConModifier;
        PlayerAttributes.Intelligence += _item.IntModifier;
        PlayerAttributes.Wisdom += _item.WisModifier;
        PlayerAttributes.Charisma += _item.ChaModifier;
        PlayerAttributes.Luck += _item.LucModifier;

        Analytics.CustomEvent("Item equipped", new Dictionary<string, object>
        {
            { _item.Name, _item.EquipmentSlot }
        });

        EquipmentHandler.UpdateAttributesUI();
        EquipmentHandler.UpdateSpendableAttributes();
    }

    public void ClearSlot()
    {
        PlayerAttributes.Strength -= _item.StrModifier;
        PlayerAttributes.Dexterity -= _item.DexModifier;
        PlayerAttributes.Constitution -= _item.ConModifier;
        PlayerAttributes.Intelligence -= _item.IntModifier;
        PlayerAttributes.Wisdom -= _item.WisModifier;
        PlayerAttributes.Charisma -= _item.ChaModifier;
        PlayerAttributes.Luck -= _item.LucModifier;

        _item = null;
        _image.sprite = _defaultSprite;

        EquipmentHandler.UpdateAttributesUI();
        EquipmentHandler.UpdateSpendableAttributes();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if(_item == null && Inventory.Instance.TemporaryItemExists() && Inventory.Instance.TemporaryItemMatchesSlot(EquipmentType))
                {
                    Inventory.Instance.EquipTemporaryItem(this);
                }
                break;

            case PointerEventData.InputButton.Right:
                if((_item != null) && !Inventory.Instance.TemporaryItemExists())
                {
                    Inventory.Instance.Unequip(this);
                }
                break;
        }
    }

    public Equipment GetItem()
    {
        return _item;
    }

    public void ReduceDurability(int reduction)
    {
        _item.CurrentDurability -= reduction;

        if(_item.CurrentDurability <= 0)
        {
            ClearSlot();
        }
    }

    public int GetDurability()
    {
        if(_item == null)
        {
            return -1;
        }

        return _item.CurrentDurability;
    }
}
