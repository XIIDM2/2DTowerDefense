using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChanged;
    public event UnityAction OnDeath;

    private int _maxHealth;
    private int _baseHealth;
    private int _currentHealth;

    /// <summary>
    /// Script Init method for single point entry - entry point can vary for different objects
    /// </summary>
    /// <param name="baseHealth"></param>
    public void Init(int baseHealth)
    {
        _baseHealth = baseHealth;
    }

    private void Start()
    {
        _maxHealth = _baseHealth; // later we can scale this amount from different sources
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Returns current health
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    /// <summary>
    /// Returns max health
    /// </summary>
    /// <returns></returns>
    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    /// <summary>
    /// Apply damage to entity
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
        OnHealthChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    /// <summary>
    /// Apply healing to entity
    /// </summary>
    /// <param name="amount"></param>
    public void HealDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

        OnHealthChanged?.Invoke();
    }


}
