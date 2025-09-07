using UnityEngine;

// this script is mainly for global control of unit behavior
public class UnitController : MonoBehaviour
{
    // Once unit died, need to invoke event for each unit (1 unit died)
    protected readonly int UNIT_DEATH_AMOUNT = 1;

    private UnitAttack _attack;
    private UnitMovement _movement;
    private Health _health;

    protected UnitPath _path;
    protected UnitAnimationsController _animation;

    protected virtual void Awake()
    {
        _attack = GetComponent<UnitAttack>();
        _movement = GetComponent<UnitMovement>();
        _health = GetComponent<Health>();
        _path = GetComponent<UnitPath>();
        _animation = GetComponentInChildren<UnitAnimationsController>();
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
        // Invoke event for scene manager that unit died by finishing path
        Messenger<int>.Broadcast(GameEvents.GlobalUnitsAmountChanged, -UNIT_DEATH_AMOUNT);
    }

    private void OnDeath()
    {
        Debug.Log("Death happened");
        _attack.enabled = false;
        _movement.enabled = false;
        // Invoke event for scene manager that unit died by being killed
        Messenger<int>.Broadcast(GameEvents.GlobalUnitsAmountChanged, -UNIT_DEATH_AMOUNT);
    }


}
