using UnityEngine.Events;

public class UnitHealth : Health
{
    public UnityAction OnHealthChanged;
    public UnityAction OnDeath;
    protected override void HealthChangedEventInvoke()
    {
        OnHealthChanged?.Invoke();
    }

    protected override void DeathEventInvoke()
    {
        OnDeath?.Invoke();
    }

}
