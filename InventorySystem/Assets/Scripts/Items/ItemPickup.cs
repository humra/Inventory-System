using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private float _interactibleDistance = 1f;
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
        Inventory.Instance.AddItem(_item);
        Destroy(gameObject);
    }
}
