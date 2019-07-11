using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerAnimator _playerAnimator;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerAnimator = FindObjectOfType<PlayerAnimator>();
    }

    private void Update()
    {
        _setAnimatorParameters();
    }

    private void _setAnimatorParameters()
    {
        _playerAnimator.SetAnimatorParameters(_playerController.GetPlayerVelocity());
    }
}

