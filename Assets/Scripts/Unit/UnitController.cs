using UnityEngine;
using VContainer;

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

    [Inject] private UnitData _data;
    
    /// <summary>
    /// Sinle Entry Initialization point for unit, use it when we spawn unit (from factory/spawner)
    /// </summary>
    /// <param name="unitData"></param>
    public void Initialize(UnitData unitData)
    {
        _attack.Init(unitData.UnitStats.DamageToPlayer);
        _movement.Init(unitData.UnitStats.MovementSpeed);
        _health.Init(unitData.UnitStats.BaseHealth);
        _animation.Init(unitData.UnitAnimations);
    }

    protected virtual void Awake()
    {
        _attack = GetComponent<UnitAttack>();
        _movement = GetComponent<UnitMovement>();
        _health = GetComponent<Health>();
        _path = GetComponent<UnitPath>();
        _animation = GetComponentInChildren<UnitAnimationsController>();

        Initialize(_data);
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
        _attack.enabled = false;
        _movement.enabled = false;
        // Invoke event for scene manager that unit died by being killed
        Messenger<int>.Broadcast(GameEvents.GlobalUnitsAmountChanged, -UNIT_DEATH_AMOUNT);
    }


}
