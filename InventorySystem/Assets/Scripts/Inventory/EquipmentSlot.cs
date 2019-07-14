using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : EventTrigger
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
        Debug.Log(item.Name + "equipped to " + EquipmentType.ToString());
        _item = item;
        _image.sprite = _item.Icon;

        PlayerAttributes.Strength += _item.StrModifier;
        PlayerAttributes.Dexterity += _item.DexModifier;
        PlayerAttributes.Constitution += _item.ConModifier;
        PlayerAttributes.Intelligence += _item.IntModifier;
        PlayerAttributes.Wisdom += _item.WisModifier;
        PlayerAttributes.Charisma += _item.ChaModifier;

        EquipmentHandler.UpdateAttributesUI();
    }

    public void ClearSlot()
    {
        PlayerAttributes.Strength -= _item.StrModifier;
        PlayerAttributes.Dexterity -= _item.DexModifier;
        PlayerAttributes.Constitution -= _item.ConModifier;
        PlayerAttributes.Intelligence -= _item.IntModifier;
        PlayerAttributes.Wisdom -= _item.WisModifier;
        PlayerAttributes.Charisma -= _item.ChaModifier;

        _item = null;
        _image.sprite = _defaultSprite;

        EquipmentHandler.UpdateAttributesUI();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Debug.Log("Left click");
                break;

            case PointerEventData.InputButton.Right:
                if(_item != null)
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
}
