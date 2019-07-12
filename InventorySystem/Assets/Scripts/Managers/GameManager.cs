﻿using UnityEngine;

public class GameManager : MonoBehaviour, IItemPickupHandler, IInventoryHandler
{
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;
    private UIManager _uiManager;
    private Inventory _inventory;

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
    }

    private void _injectInterfaceDependencies()
    {
        foreach(ItemPickup itemPickup in GameObject.FindObjectsOfType<ItemPickup>())
        {
            itemPickup.ItemPickupHandler = this;
        }

        _inventory.InventorySlotHandler = this;
    }

    private void _setAnimatorParameters()
    {
        _playerAnimator.SetAnimatorParameters(_playerController.GetPlayerVelocity());
    }

    public bool IsPlayerWithinInteractibleRange(Vector3 itemPosition, float interactibleDistance)
    {
        return Vector3.Distance(itemPosition, _playerController.transform.position) <= interactibleDistance;
    }

    public void UpdateInventoryUI()
    {
        _uiManager.UpdateInventory();
    }
}

public interface IItemPickupHandler
{
    bool IsPlayerWithinInteractibleRange(Vector3 itemPosition, float interactibleDistance);
}

public interface IInventoryHandler
{
    void UpdateInventoryUI();
}