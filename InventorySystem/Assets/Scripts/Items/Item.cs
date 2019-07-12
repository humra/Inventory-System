using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public EnumPickupEvent PickupEvent;
}

public enum EnumPickupEvent
{
    EnumPermanentUsage, EnumPickupToInventory
}

public enum EnumEquippable
{
    EnumEquippable, EnumNonEquippable
}

public enum EnumStackable
{
    EnumNonStackable, EnumStackableInfinite, EnumStackableN
}
