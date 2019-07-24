using UnityEngine;

public class DurabilityManager : MonoBehaviour
{
    [SerializeField]
    private int _durabilityReductionFactor = 10;
    [SerializeField]
    private float _reductionDistance = 1f;

    private EquipmentSlot[] _equipmentSlots;
    private Transform _player;
    private Vector3 _lastPosition;

    private void Start()
    {
        _equipmentSlots = GameObject.FindObjectsOfType<EquipmentSlot>();
        _player = GameObject.FindObjectOfType<PlayerController>().transform;
        _lastPosition = _player.position;
    }

    private void Update()
    {
        if(Mathf.Abs(Vector3.Distance(_lastPosition, _player.position)) < _reductionDistance)
        {
            return;
        }

        for(int i = 0; i < _equipmentSlots.Length; i++)
        {
            if(_equipmentSlots[i].GetItem() != null)
            {
                _equipmentSlots[i].ReduceDurability(_durabilityReductionFactor);
            }
        }

        _lastPosition = _player.position;
    }
}
