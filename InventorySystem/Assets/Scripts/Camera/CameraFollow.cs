using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, -10);

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        transform.position = _player.position + _offset;
    }
}
