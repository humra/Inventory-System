using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Pickup", menuName = "Inventory/ConsumablePickup")]
public class ConsumablePickup : Item
{
    public int StrModifier;
    public int DexModifier;
    public int ConModifier;
    public int IntModifier;
    public int WisModifier;
    public int ChaModifier;
}
