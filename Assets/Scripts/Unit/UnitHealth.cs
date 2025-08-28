using UnityEngine.Events;
using VContainer;

// Class where we register dependencies for each unit and invoke Units events
public class UnitHealth : Health
{
    public event UnityAction OnHealthChanged;
    public event UnityAction OnDeath;

    [Inject] private readonly UnitData _UnitData;

    protected override void Start()
    {
        _maxHealth = _UnitData.MaxHealth;

        base.Start();
    }

    protected override void HealthChangedEventInvoke()
    {
        OnHealthChanged?.Invoke();
    }

    protected override void DeathEventInvoke()
    {
        OnDeath?.Invoke();
    }

}
