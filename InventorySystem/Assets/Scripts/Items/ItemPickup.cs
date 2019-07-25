using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private float _interactibleDistance = 1.5f;
    [SerializeField]
    private float _overlapCircleInteractibleDistance = 0.25f;
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

    #region pickup_options

    private void OnMouseDown()
    {
        if(ItemPickupHandler.IsPlayerWithinInteractibleRange(transform.position, _interactibleDistance) 
            && ItemPickupHandler.GetPickupMethod() == EnumPickupMethod.EnumClick)
        {
            _pickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && ItemPickupHandler.GetPickupMethod() == EnumPickupMethod.EnumTriggerCollision)
        {
            _pickUp();
        }
    }

    private void Update()
    {
        if(ItemPickupHandler.GetPickupMethod() != EnumPickupMethod.EnumPhysicsOverlapCircle)
        {
            return;
        }

        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, _overlapCircleInteractibleDistance);
        if(collisions.Length > 0)
        {
            foreach(Collider2D collider in collisions)
            {
                if (collider.gameObject.tag.Equals("Player"))
                {
                    _pickUp();
                }
            }
        }
    }

    #endregion

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
        Analytics.CustomEvent("Item picked up", new Dictionary<string, object>
        {
            { _item.Name, _item.GetType().ToString() }
        });

        switch (_item.PickupEvent)
        {
            case EnumPickupEvent.EnumPermanentUsage:
                ConsumablePickup consumable = (ConsumablePickup)_item;
                PlayerAttributes.Strength += consumable.StrModifier;
                PlayerAttributes.Dexterity += consumable.DexModifier;
                PlayerAttributes.Constitution += consumable.ConModifier;
                PlayerAttributes.Intelligence += consumable.IntModifier;
                PlayerAttributes.Wisdom += consumable.WisModifier;
                PlayerAttributes.Charisma += consumable.ChaModifier;
                PlayerAttributes.Luck += consumable.LucModifier;
                ItemPickupHandler.UpdateAttributesUI();

                Debug.Log(_item.name + " picked up.");
                ItemPickupHandler.ShowInfoMessage(_item.name + " picked up.");

                Analytics.CustomEvent("Item used ", new Dictionary<string, object>
                {
                    { _item.Name, _item.GetType().ToString() }
                });

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
