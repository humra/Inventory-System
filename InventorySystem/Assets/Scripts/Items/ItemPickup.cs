using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private float _interactibleDistance = 1.5f;
    [SerializeField]
    private Item _item;

    public IItemPickupHandler ItemPickupHandler;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _item.Icon;
    }

    private void OnMouseDown()
    {
        if(ItemPickupHandler.IsPlayerWithinInteractibleRange(transform.position, _interactibleDistance))
        {
            _pickUp();
        }
    }

    private void _pickUp()
    {
        switch(_item.PickupEvent)
        {
            case EnumPickupEvent.EnumPermanentUsage:
                ConsumablePickup consumable = (ConsumablePickup)_item;
                PlayerAttributes.Strength += consumable.StrModifier;
                PlayerAttributes.Dexterity += consumable.DexModifier;
                PlayerAttributes.Constitution += consumable.ConModifier;
                PlayerAttributes.Intelligence += consumable.IntModifier;
                PlayerAttributes.Wisdom += consumable.WisModifier;
                PlayerAttributes.Charisma += consumable.ChaModifier;
                ItemPickupHandler.UpdateAttributesUI();

                Debug.Log(_item.name + " picked up.");
                Destroy(gameObject);
                break;

            case EnumPickupEvent.EnumPickupToInventory:
                if (Inventory.Instance.AddItem(_item))
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
