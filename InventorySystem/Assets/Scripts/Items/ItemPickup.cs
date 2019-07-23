using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private float _interactibleDistance = 1.5f;
    [SerializeField]
    private Item _item;

    private SpriteRenderer _image;
    private Color _originalColor;

    public IItemPickupHandler ItemPickupHandler;

    private void Start()
    {
        _image = GetComponent<SpriteRenderer>();

        if(_item != null)
        {
            _setSprite();
        }

        _originalColor = _image.color;
    }

    private void OnMouseDown()
    {
        if(ItemPickupHandler.IsPlayerWithinInteractibleRange(transform.position, _interactibleDistance))
        {
            _pickUp();
        }
    }

    private void _setSprite()
    {
        if(_item == null)
        {
            return;
        }

        _image.sprite = _item.Icon;
    }

    private void _highlight()
    {
        _image.color = Color.cyan;
    }

    private void _stopHighlight()
    {
        _image.color = _originalColor;
    }

    private void OnMouseEnter()
    {
        _highlight();
    }

    private void OnMouseExit()
    {
        _stopHighlight();
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
                ItemPickupHandler.ShowInfoMessage(_item.name + " picked up.");
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

    public void SetItem(Item newItem)
    {
        _item = newItem;
        _image = GetComponent<SpriteRenderer>();
        _setSprite();
    }
}
