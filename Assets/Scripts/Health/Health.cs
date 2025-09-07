using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class Health : MonoBehaviour
{
    public event UnityAction OnHealthChanged;
    public event UnityAction OnDeath;

    private int _maxHealth;
    private int _currentHealth;

    // we inject ihealthconfig via vcontainer, contract can be via different scriptableobjects such as playerdata,unitdata,towerdata etc,
    // we need to register service as ihealthconfig aside as required data (make multiregistration service)
    [Inject] private IHealthConfig _healthData;

    // we assign max health with basehealth of ihealthconfig instance which we recieved through scriptableobject data which has contract ihealthconfig
    private void Start()
    {
        _maxHealth = _healthData.BaseHealth;
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
