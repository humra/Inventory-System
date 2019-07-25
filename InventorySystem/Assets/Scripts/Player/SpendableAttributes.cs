using UnityEngine;

public class SpendableAttributes : MonoBehaviour
{
    [SerializeField]
    private int _originalHP = 100;
    [SerializeField]
    private int _originalMP = 100;

    private int _maxHP;
    private int _maxMP;
    private int _currentHP;
    private int _currentMP;

    private void Start()
    {
        _maxHP = _originalHP;
        _currentHP = _maxHP;
        _maxMP = _originalMP;
        _currentMP = _maxMP;

        UpdateMaxStats();
    }

    public void UpdateMaxStats()
    {
        _maxHP = _originalHP + (PlayerAttributes.Constitution * 10);
        _maxMP = _originalMP + (PlayerAttributes.Intelligence * 10);

        _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
        _currentMP = Mathf.Clamp(_currentMP, 0, _maxMP);
    }

    public void LoseHP(int amount)
    {
        _currentHP -= amount;
        _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
    }

    public void GainHP(int amount)
    {
        _currentHP += amount;
        _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
    }

    public void LoseMP(int amount)
    {
        _currentMP -= amount;
        _currentMP = Mathf.Clamp(_currentMP, 0, _maxMP);
    }

    public void GainMP(int amount)
    {
        _currentMP += amount;
        _currentMP = Mathf.Clamp(_currentMP, 0, _maxMP);
    }
}