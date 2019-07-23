using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private Transform _temporaryTarget;
    private float _temporaryFocusDuration;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, -10);
    [SerializeField]
    private float _followSpeed = 3f;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _temporaryTarget = null;
    }

    private void LateUpdate()
    {
        if(_temporaryTarget != null)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _temporaryTarget.transform.position.x, Time.deltaTime * _followSpeed),
            Mathf.Lerp(transform.position.y, _temporaryTarget.transform.position.y, Time.deltaTime * _followSpeed)) + _offset;

            _temporaryFocusDuration -= Time.deltaTime;

            if(_temporaryFocusDuration <= 0)
            {
                ClearTemporaryTarget();
            }

            return;
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _player.transform.position.x, Time.deltaTime * _followSpeed),
            Mathf.Lerp(transform.position.y, _player.transform.position.y, Time.deltaTime * _followSpeed)) + _offset;
    }

    public void FocusOnTarget(Transform newTarget, float focusDuration)
    {
        _temporaryTarget = newTarget;
        _temporaryFocusDuration = focusDuration;
    }

    public void ClearTemporaryTarget()
    {
        _temporaryFocusDuration = 0f;
        _temporaryTarget = null;
    }
}
