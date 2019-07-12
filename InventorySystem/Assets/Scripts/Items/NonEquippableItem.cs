using UnityEngine;

[CreateAssetMenu(fileName = "New NonEquippable Item", menuName = "Inventory/Non-Equippable")]
public class NonEquippableItem : Item
{
    public EnumStackable Stackable;
    public int MaximumStack;
}
