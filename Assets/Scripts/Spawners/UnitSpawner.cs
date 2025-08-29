using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField, Range(0.0f, 2.0f)] private float _positionOffsetValue = 2f;

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
    }

    // Slightly changes spawn position to make waves more "alive"
    private Vector2 GetRandomSpawnPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * _positionOffsetValue;

        return new Vector2(transform.position.x + randomOffset.x, transform.position.y + randomOffset.y);
    }
}
