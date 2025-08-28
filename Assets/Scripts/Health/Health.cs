using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected int _maxHealth;
    private int _currentHealth;

    // Set health data (maxHealth in inherit classes)
    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }
    // abstract events methods for different logic for different classes
    protected abstract void HealthChangedEventInvoke();

    protected abstract void DeathEventInvoke();

    /// <summary>
    /// Returns currentHealth
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    /// <summary>
    /// Returns maxHealth
    /// </summary>
    /// <returns></returns>
    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    /// <summary>
    /// Apply damage to instance
    /// </summary>
    /// <param name="amount"></param>
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

    /// <summary>
    /// Apply healing to instance
    /// </summary>
    /// <param name="amount"></param>
    public void HealDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

        HealthChangedEventInvoke();
    }


}
