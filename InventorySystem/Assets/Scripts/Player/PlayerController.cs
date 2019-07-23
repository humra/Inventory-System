using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _disableInput = false;
    private float _disableDuration = 0f;

    [SerializeField]
    private float _movementSpeed = 5;
    [SerializeField]
    private float _interpolation = 1;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_disableInput)
        {
            _disableDuration -= Time.deltaTime;

            if(_disableDuration <= 0)
            {
                _disableInput = false;
            }

            return;
        }

        Vector2 movementVector = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * _movementSpeed, _interpolation),
        Mathf.Lerp(0, Input.GetAxis("Vertical") * _movementSpeed, _interpolation));
        _rigidbody.velocity = movementVector.normalized * _movementSpeed;
    }

    public Vector2 GetPlayerVelocity()
    {
        return _rigidbody.velocity;
    }

    public void DisableInputForDuration(float duration)
    {
        _disableDuration = duration;
        _disableInput = true;
    }
}
