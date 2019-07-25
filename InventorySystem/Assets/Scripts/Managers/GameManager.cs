using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System;
using System.Collections;

public class GameManager : MonoBehaviour, IItemPickupHandler, IInventoryInteractionHandler, IItemHoverHandler, IEquipmentHandler, IFocusableObjectHandler
{
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;
    private UIManager _uiManager;
    private Inventory _inventory;
    private EnumPickupMethod _pickupMethod;
    private float _currentDistanceTravelled = 0;
    private int _timesDistanceTracked = 0;
    private Vector3 _lastPlayerPosition;
    private SpendableAttributes _spendableAttributes;

    [SerializeField]
    private float _distanceTrackingIncrement = 10;
    [SerializeField]
    private int _maxDistanceTrackedNumber = 5;

    public GameObject PickupPrefab;
    public Item[] ExistingItems;

    private void Start()
    {
        Analytics.CustomEvent("Game Started", new Dictionary<string, object>
        {
            { Application.platform.ToString(), DateTime.Now }
        });

        _playerController = FindObjectOfType<PlayerController>();
        _playerAnimator = FindObjectOfType<PlayerAnimator>();
        _uiManager = FindObjectOfType<UIManager>();
        _inventory = FindObjectOfType<Inventory>();
        _lastPlayerPosition = _playerController.transform.position;
        _spendableAttributes = FindObjectOfType<SpendableAttributes>();

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


        if(_timesDistanceTracked < _maxDistanceTrackedNumber)
        {
            _currentDistanceTravelled += Mathf.Abs(Vector3.Distance(_lastPlayerPosition, _playerController.transform.position));
            _lastPlayerPosition = _playerController.transform.position;

            if(_currentDistanceTravelled >= _distanceTrackingIncrement)
            {
                _timesDistanceTracked++;
                _currentDistanceTravelled = 0;

                Analytics.CustomEvent("Player travelled 10 units");
            }
        }
    }

    private void _instantiateRandomItem()
    {
        GameObject newPickup = Instantiate(PickupPrefab, _playerController.transform.position, Quaternion.identity);
        newPickup.GetComponent<ItemPickup>().SetItem(ExistingItems[UnityEngine.Random.Range(0, ExistingItems.Length)]);
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

    #region coroutines

    private IEnumerator _modifyAttributeForSetTime(EnumAttributes attribute, int factor, float duration)
    {
        switch(attribute)
        {
            case EnumAttributes.EnumStr:
                PlayerAttributes.Strength += factor;
                break;
            case EnumAttributes.EnumDex:
                PlayerAttributes.Dexterity += factor;
                break;
            case EnumAttributes.EnumCon:
                PlayerAttributes.Constitution += factor;
                break;
            case EnumAttributes.EnumInt:
                PlayerAttributes.Intelligence += factor;
                break;
            case EnumAttributes.EnumWis:
                PlayerAttributes.Wisdom += factor;
                break;
            case EnumAttributes.EnumCha:
                PlayerAttributes.Charisma += factor;
                break;
            case EnumAttributes.EnumLuc:
                PlayerAttributes.Luck += factor;
                break;
        }

        _uiManager.UpdateAttributes();

        yield return new WaitForSeconds(duration);

        switch (attribute)
        {
            case EnumAttributes.EnumStr:
                PlayerAttributes.Strength -= factor;
                break;
            case EnumAttributes.EnumDex:
                PlayerAttributes.Dexterity -= factor;
                break;
            case EnumAttributes.EnumCon:
                PlayerAttributes.Constitution -= factor;
                break;
            case EnumAttributes.EnumInt:
                PlayerAttributes.Intelligence -= factor;
                break;
            case EnumAttributes.EnumWis:
                PlayerAttributes.Wisdom -= factor;
                break;
            case EnumAttributes.EnumCha:
                PlayerAttributes.Charisma -= factor;
                break;
            case EnumAttributes.EnumLuc:
                PlayerAttributes.Luck -= factor;
                break;
        }

        _uiManager.UpdateAttributes();
    }

    private IEnumerator _rampUpValueThenHold(EnumAttributes attribute, int factor, float duration, float rampTime)
    {
        float rampTick = rampTime / factor;
        int factorCounter = 0;

        while(factorCounter < factor)
        {
            yield return new WaitForSeconds(rampTick);

            switch (attribute)
            {
                case EnumAttributes.EnumStr:
                    PlayerAttributes.Strength++;
                    break;
                case EnumAttributes.EnumDex:
                    PlayerAttributes.Dexterity++;
                    break;
                case EnumAttributes.EnumCon:
                    PlayerAttributes.Constitution++;
                    break;
                case EnumAttributes.EnumInt:
                    PlayerAttributes.Intelligence++;
                    break;
                case EnumAttributes.EnumWis:
                    PlayerAttributes.Wisdom++;
                    break;
                case EnumAttributes.EnumCha:
                    PlayerAttributes.Charisma++;
                    break;
                case EnumAttributes.EnumLuc:
                    PlayerAttributes.Luck++;
                    break;
            }

            _uiManager.UpdateAttributes();
            factorCounter++;
        }

        yield return new WaitForSeconds(duration);

        switch (attribute)
        {
            case EnumAttributes.EnumStr:
                PlayerAttributes.Strength -= factor;
                break;
            case EnumAttributes.EnumDex:
                PlayerAttributes.Dexterity -= factor;
                break;
            case EnumAttributes.EnumCon:
                PlayerAttributes.Constitution -= factor;
                break;
            case EnumAttributes.EnumInt:
                PlayerAttributes.Intelligence -= factor;
                break;
            case EnumAttributes.EnumWis:
                PlayerAttributes.Wisdom -= factor;
                break;
            case EnumAttributes.EnumCha:
                PlayerAttributes.Charisma -= factor;
                break;
            case EnumAttributes.EnumLuc:
                PlayerAttributes.Luck -= factor;
                break;
        }

        _uiManager.UpdateAttributes();
    }

    private IEnumerator _tickUpValue(EnumAttributes attribute, int factor, int numberOfTicks, float tickTime)
    {
        for(int i = 1; i <= numberOfTicks; i++)
        {
            switch(attribute)
            {
                case EnumAttributes.EnumHP:
                    _spendableAttributes.GainHP(factor);
                    break;
                case EnumAttributes.EnumMP:
                    _spendableAttributes.GainMP(factor);
                    break;
            }

            yield return new WaitForSeconds(tickTime);
        }
    }

    #endregion

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

    public void UpdateSpendableAttributes()
    {
        _spendableAttributes.UpdateMaxStats();
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

    public void HoldValue(Consumable consumable)
    {
        StartCoroutine(_modifyAttributeForSetTime(consumable.ModifiedAttribute, consumable.Factor, consumable.Duration));
    }

    public void RampUpValue(Consumable consumable)
    {
        StartCoroutine(_rampUpValueThenHold(consumable.ModifiedAttribute, consumable.Factor, consumable.Duration, consumable.RampTime));
    }

    public void TickUpValue(Consumable consumable)
    {
        StartCoroutine(_tickUpValue(consumable.ModifiedAttribute, consumable.Factor, consumable.NumberOfTicks, consumable.TickTime));
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
    void HoldValue(Consumable consumable);
    void RampUpValue(Consumable consumable);
    void TickUpValue(Consumable consumable);
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
    void UpdateSpendableAttributes();
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