using UnityEngine;

public class SlimeController : UnitController
{
    [SerializeField] private Transform _leftSpawnPosition;
    [SerializeField] private Transform _rightSpawnPosition;

    private const UnitType UNIT_TYPE_BABY_SLIME = UnitType.BabySlime;

    private bool _isBabySlimesSpawned = false;

    private SlimeAnimation _slimeAnimation;

    protected override void Awake()
    {
        base.Awake();

        _slimeAnimation = _animation as SlimeAnimation;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _slimeAnimation.OnSlimeSplit += Split;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _slimeAnimation.OnSlimeSplit -= Split;
    }

    // Splits slime into 2 babySlimes
    private void Split()
    {
        if (_isBabySlimesSpawned) return;

        _isBabySlimesSpawned = true;

        // event for spawner to spawn babyslime in position with slime`s pathtype and pathpoint index
        Messenger<UnitType, Vector2?, (PathType, int)>.Broadcast(GameEvents.SpawnUnit, UNIT_TYPE_BABY_SLIME, _leftSpawnPosition.position, (_path.GetPathData()));
        Messenger<UnitType, Vector2?, (PathType, int)>.Broadcast(GameEvents.SpawnUnit, UNIT_TYPE_BABY_SLIME, _rightSpawnPosition.position, (_path.GetPathData()));
    }
}
