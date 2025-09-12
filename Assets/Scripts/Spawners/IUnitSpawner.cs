using UnityEngine;

public interface IUnitSpawner : ISpawner
{
    void Spawn(UnitType unitType, Vector2? position, (PathType pathType, int pathPointIndex) pathInfo);
}
