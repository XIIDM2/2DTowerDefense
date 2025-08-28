using UnityEngine;

// this script is mainly for global control of unit behavior
public class UnitController : MonoBehaviour
{
    private UnitAttack _attack;
    private UnitMovement _movement;
    private UnitHealth _health;
    private UnitPath _path;
    private UnitAnimation _animation;

    private void Awake()
    {
        _attack = GetComponent<UnitAttack>();
        _movement = GetComponent<UnitMovement>();
        _health = GetComponent<UnitHealth>();
        _path = GetComponent<UnitPath>();
        _animation = GetComponent<UnitAnimation>();
    }

    private void OnEnable()
    {
        _health.OnDeath += OnDeath;
        _path.OnPathEnd += OnPathEnd;
    }

    private void OnDisable()
    {
        _health.OnDeath -= OnDeath;
        _path.OnPathEnd -= OnPathEnd;
    }

    private void OnPathEnd()
    {
        _attack.ApplyDamageToPlayer();
    }

    private void OnDeath()
    {
        _attack.enabled = false;
        _movement.enabled = false;
    }
}
