using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private float _movementX;
    private float _movementY;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("MovementX", _movementX);
        _animator.SetFloat("MovementY", _movementY);
    }

    public void SetAnimatorParameters(Vector2 playerVelocity)
    {
        _movementX = playerVelocity.x;
        _movementY = playerVelocity.y;
    }
}
