using UnityEngine;

public abstract class Health : MonoBehaviour
{
    private int _maxHealth;
    private int _currentHealth;

    //[Inject] private readonly CharacterData _characterData;

    // Set health data
    private void Awake()
    {
        _maxHealth = 10;
        _currentHealth = _maxHealth;
    }
    protected abstract void HealthChangedEventInvoke();

    protected abstract void DeathEventInvoke();

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    // Apply damage to instance
    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
        HealthChangedEventInvoke();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            DeathEventInvoke();
        }
    }

    // Apply healing to instance
    public void HealDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

        HealthChangedEventInvoke();
    }


}
