using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public EnumAttributes ModifiedAttribute;
    public EnumConsumableType ConsumableType;
    public int Factor;
    public float Duration;
    public float RampTime;
    public float TickTime;
    public float NumberOfTicks;
}

public enum EnumConsumableType
{
    EnumHold, EnumTick, EnumRamp
}