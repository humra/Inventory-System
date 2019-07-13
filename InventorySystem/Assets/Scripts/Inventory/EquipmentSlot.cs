using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    private Equipment _item;
    private Image _image;

    public EnumEquipmentSlot EquipmentType;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void SetItem(Equipment item)
    {
        Debug.Log(item.Name + "equipped to " + EquipmentType.ToString());
        _item = item;
        _image.sprite = _item.Icon;
    }

    public Equipment GetItem()
    {
        return _item;
    }
}
