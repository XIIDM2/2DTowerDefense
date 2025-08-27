using UnityEngine;

public class Spawner : MonoBehaviour
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
        Messenger<UnitType, PathType>.AddListener(GameEvents.SpawnUnit, SpawnUnit);
    }

    // We resighn from a global event from messenger
    private void OnDisable()
    {
        Messenger<UnitType, PathType>.RemoveListener(GameEvents.SpawnUnit, SpawnUnit);
    }

    // Spawn unit created from factory (spawn position with random offset - check GetRandomSpawnPoint() method)
    public async void SpawnUnit(UnitType unitType, PathType pathType)
    {
        GameObject unitPrefab = await (_factory.CreateUnit(unitType));

        GameObject unit = Instantiate(unitPrefab, GetRandomSpawnPoint(), Quaternion.identity);

        unit.GetComponent<UnitPath>().SetPath(pathType);
    }

    // Slightly changes spawn position to make waves more "alive"
    private Vector2 GetRandomSpawnPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * _positionOffsetValue;

        return new Vector2(transform.position.x + randomOffset.x, transform.position.y + randomOffset.y);
    }
}
