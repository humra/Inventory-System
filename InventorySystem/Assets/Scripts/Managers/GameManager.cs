using UnityEngine;

public class GameManager : MonoBehaviour, IItemPickupHandler
{
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerAnimator = FindObjectOfType<PlayerAnimator>();

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
    }

    private void _setAnimatorParameters()
    {
        _playerAnimator.SetAnimatorParameters(_playerController.GetPlayerVelocity());
    }

    public bool IsPlayerWithinInteractibleRange(Vector3 itemPosition, float interactibleDistance)
    {
        return Vector3.Distance(itemPosition, _playerController.transform.position) <= interactibleDistance;
    }
}

public interface IItemPickupHandler
{
    bool IsPlayerWithinInteractibleRange(Vector3 itemPosition, float interactibleDistance);
}