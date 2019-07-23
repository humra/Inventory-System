using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, -10);
    [SerializeField]
    private float _followSpeed = 3f;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _player.transform.position.x, Time.deltaTime * _followSpeed),
            Mathf.Lerp(transform.position.y, _player.transform.position.y, Time.deltaTime * _followSpeed)) + _offset;
    }
}
