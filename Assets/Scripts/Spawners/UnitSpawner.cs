using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField, Range(0.0f, 2.0f)] private float _positionOffsetValue = 2f;

    // Once unit spawned, need to invoke event for each unit (1 unit spawned)
    private const int UNIT_SPAWN_AMOUNT = 1;

    private Factory _factory;

    private void Start()
    {
        _factory = new Factory();
    }

    // We sign for a global event from messenger to spawn a unit when event happens
    private void OnEnable()
    {
        Messenger<UnitType, Vector2?, (PathType, int)>.AddListener(GameEvents.SpawnUnit, SpawnUnit);
    }

    // We resign from a global event from messenger
    private void OnDisable()
    {
        Messenger<UnitType, Vector2?, (PathType, int)>.RemoveListener(GameEvents.SpawnUnit, SpawnUnit);
    }

    /// <summary>
    /// Spawn unit created from factory and assign path to walk
    /// </summary>
    /// <param name="unitType"></param>
    /// <param name="position"></param>
    /// <param name="pathType"></param>
    public async void SpawnUnit(UnitType unitType, Vector2? position, (PathType pathType, int pathPointIndex) pathInfo)
    {
        GameObject unitPrefab = await (_factory.CreateUnit(unitType));

        GameObject unit = null;

        // spawn on default position (spawner position) with random offset - check GetRandomSpawnPoint() method
        if (!position.HasValue)
        {
            unit = Instantiate(unitPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        }
        // or spawn at requested position
        else
        {
            unit = Instantiate(unitPrefab, position.Value, Quaternion.identity);
        }

        unit.GetComponent<UnitPath>().SetPath(pathInfo.pathType, pathInfo.pathPointIndex);

        // Increase global unit Amount if spawned unit succesfully
        if (unit != null)
        {
            // Invoke event for scene manager that unit spawned
            switch (unitType)
            {
                // If unit is slime we add 2 for total amount since after death 2 baby slimes will appear (magic number 2 = amount of baby slimes, BE CAREFUL!)
                case UnitType.Slime:
                    Messenger<int>.Broadcast(GameEvents.GlobalUnitsAmountChanged, UNIT_SPAWN_AMOUNT + 2);
                    break;
                    // if babyslime, we skip adding for total amount because already added in slime case
                case UnitType.BabySlime:
                    break;
                default:
                    Messenger<int>.Broadcast(GameEvents.GlobalUnitsAmountChanged, UNIT_SPAWN_AMOUNT);
                    break;
            }

            // solve the issue to be able to spawn babyslime by default
        }
    }

    // Slightly changes spawn position to make waves more "alive"
    private Vector2 GetRandomSpawnPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * _positionOffsetValue;

        return new Vector2(transform.position.x + randomOffset.x, transform.position.y + randomOffset.y);
    }
}
