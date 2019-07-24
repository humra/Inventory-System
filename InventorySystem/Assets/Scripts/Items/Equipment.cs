using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EnumEquipmentSlot EquipmentSlot;
    public EnumEquippable Equippable;
    public EnumStackable Stackable;
    public int StrModifier;
    public int DexModifier;
    public int ConModifier;
    public int IntModifier;
    public int WisModifier;
    public int ChaModifier;
}

public enum EnumEquipmentSlot
{
    EnumHead, EnumChest, EnumLegs, EnumRHand, EnumLHand, EnumBoots, EnumRing
}
