using UnityEngine;

public class GameManager : MonoBehaviour, IItemPickupHandler, IInventoryInteractionHandler, IItemHoverHandler, IEquipmentHandler, IFocusableObjectHandler
{
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;
    private UIManager _uiManager;
    private Inventory _inventory;
    private EnumPickupMethod _pickupMethod;

    public GameObject PickupPrefab;
    public Item[] ExistingItems;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerAnimator = FindObjectOfType<PlayerAnimator>();
        _uiManager = FindObjectOfType<UIManager>();
        _inventory = FindObjectOfType<Inventory>();

        _injectInterfaceDependencies();
    }

    private void Update()
    {
        _setAnimatorParameters();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _instantiateRandomItem();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pickup method: " + EnumPickupMethod.EnumClick);
            _pickupMethod = EnumPickupMethod.EnumClick;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Pickup method: " + EnumPickupMethod.EnumTriggerCollision);
            _pickupMethod = EnumPickupMethod.EnumTriggerCollision;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Pickup method: " + EnumPickupMethod.EnumPhysicsOverlapCircle);
            _pickupMethod = EnumPickupMethod.EnumPhysicsOverlapCircle;
        }
    }

    private void _instantiateRandomItem()
    {
        GameObject newPickup = Instantiate(PickupPrefab, _playerController.transform.position, Quaternion.identity);
        newPickup.GetComponent<ItemPickup>().SetItem(ExistingItems[Random.Range(0, ExistingItems.Length)]);
        newPickup.GetComponent<ItemPickup>().ItemPickupHandler = this;
    }

    private void _injectInterfaceDependencies()
    {
        foreach(ItemPickup itemPickup in GameObject.FindObjectsOfType<ItemPickup>())
        {
            itemPickup.ItemPickupHandler = this;
        }

        foreach(InventorySlot inventorySlot in GameObject.FindObjectsOfType<InventorySlot>())
        {
            inventorySlot.ItemHoverHandler = this;
        }

        foreach(EquipmentSlot equipmentSlot in GameObject.FindObjectsOfType<EquipmentSlot>())
        {
            equipmentSlot.EquipmentHandler = this;
        }

        foreach(FocusableObject focusableObject in GameObject.FindObjectsOfType<FocusableObject>())
        {
            focusableObject.focusableObjectHandler = this;
        }

        Inventory.Instance.InventoryInteractionHandler = this;
        Inventory.Instance.ItemHoverHandler = this;
    }

    private void _setAnimatorParameters()
    {
        _playerAnimator.SetAnimatorParameters(_playerController.GetPlayerVelocity());
    }

    public bool IsPlayerWithinInteractibleRange(Vector3 itemPosition, float interactibleDistance)
    {
        return Vector3.Distance(itemPosition, _playerController.transform.position) <= interactibleDistance;
    }

    public EnumPickupMethod GetPickupMethod()
    {
        return _pickupMethod;
    }

    public void UpdateAttributesUI()
    {
        _uiManager.UpdateAttributes();
    }

    public void DropItem(Item item)
    {
        GameObject newPickup = Instantiate(PickupPrefab, _playerController.transform.position, Quaternion.identity);
        newPickup.GetComponent<ItemPickup>().SetItem(item);
        newPickup.GetComponent<ItemPickup>().ItemPickupHandler = this;
    }

    public void ShowItemInfo(Item item)
    {
        _uiManager.ShowHoverText(item.Name);
    }

    public void ShowEquipmentInfo(Equipment equipment)
    {
        _uiManager.ShowHoverText(equipment.Name, equipment.MaxDurability, equipment.CurrentDurability);
    }

    public void StopShowingItemInfo()
    {
        _uiManager.HideHoverText();
    }

    public void ShowInfoMessage(string message)
    {
        _uiManager.ShowInfoMessage(message);
    }

    public void SetFocus(Transform objectPosition, float focusDuration)
    {
        _playerController.DisableInputForDuration(focusDuration);
        Camera.main.GetComponent<CameraFollow>().FocusOnTarget(objectPosition, focusDuration);
    }
}

#region interfaces

public interface IItemPickupHandler
{
    bool IsPlayerWithinInteractibleRange(Vector3 itemPosition, float interactibleDistance);
    void UpdateAttributesUI();
    void ShowInfoMessage(string message);
    EnumPickupMethod GetPickupMethod();
}

public interface IInventoryInteractionHandler
{
    void DropItem(Item item);
    void ShowInfoMessage(string message);
}

public interface IItemHoverHandler
{
    void ShowItemInfo(Item item);
    void StopShowingItemInfo();
    void ShowEquipmentInfo(Equipment equipment);
}

public interface IEquipmentHandler
{
    void UpdateAttributesUI();
}

public interface IFocusableObjectHandler
{
    void SetFocus(Transform objectPosition, float focusDuration);
}

#endregion

public enum EnumPickupMethod
{
    EnumClick, EnumTriggerCollision, EnumPhysicsOverlapCircle
}