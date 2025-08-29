using UnityEngine;

// Singleton for player instance
public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerHealth Health => _health;

    private PlayerHealth _health;

    protected override void Awake()
    {
        base.Awake();

        _health = GetComponent<PlayerHealth>();
    }

    public void TMApplyDamageToAllUnits()
    {
        var units = FindObjectsByType<UnitHealth>(FindObjectsSortMode.None);

        foreach (var unit in units)
        {
            unit.TakeDamage(100);
        }

    }
}
