using UnityEngine;

// this script is mainly for global control of unit behavior
public class UnitController : MonoBehaviour
{
    private UnitAttack _attack;
    private UnitMovement _movement;
    private UnitHealth _health;
    protected UnitPath _path;
    protected UnitAnimation _animation;

    protected virtual void Awake()
    {
        _attack = GetComponent<UnitAttack>();
        _movement = GetComponent<UnitMovement>();
        _health = GetComponent<UnitHealth>();
        _path = GetComponent<UnitPath>();
        _animation = GetComponentInChildren<UnitAnimation>();
    }

    protected virtual void OnEnable()
    {
        _health.OnDeath += OnDeath;
        _path.OnPathEnd += OnPathEnd;
    }

    protected virtual void OnDisable()
    {
        _health.OnDeath -= OnDeath;
        _path.OnPathEnd -= OnPathEnd;
    }

    private void OnPathEnd()
    {
        _attack.ApplyDamageToPlayer();
    }

    protected virtual void OnDeath()
    {
        _attack.enabled = false;
        _movement.enabled = false;
    }
}
