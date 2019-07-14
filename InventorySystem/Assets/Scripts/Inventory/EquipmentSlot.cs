using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    private Equipment _item;
    private Image _image;

    public EnumEquipmentSlot EquipmentType;
    public IEquipmentHandler EquipmentHandler;

    private void Start()
    {
        _image = GetComponent<Image>();
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

    public Equipment GetItem()
    {
        return _item;
    }
}
