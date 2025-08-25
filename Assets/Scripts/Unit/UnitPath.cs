using UnityEngine;

public class UnitPath : MonoBehaviour
{
    public Vector3 PathPointPosition => _currentPathpointPosition;

    [SerializeField] private PathTypes _requestedPathType;
    [SerializeField] private float _distanceToNextPoint = 0.5f; // 0.5f is base value

    /// <summary>
    ///  value to offset pathpoint position to make units move a little random so game feels "live"
    /// </summary>
    [SerializeField] private float _positionOffset = 0.5f; // 0.5f is base value

    private Path _path;
    private int _pathpointIndex = 0;
    private Transform _currentPathpoint;
    private Vector3 _currentPathpointPosition; // Saving copy of currentPathpoint position to avoid shuffling path, insted changing copy 

    // Setting path and first pathpoint
    private void Start()
    {
        SetPath();
        UpdatePathPoint();
        SetOffsetPathPointPosition();
    }

    // Check in update if need to update pathpoint and Set offset
    private void Update()
    {
        if (Vector2.Distance(transform.position, _currentPathpoint.position) < _distanceToNextPoint)
        {
            SetNextPoint();
            UpdatePathPoint();
            SetOffsetPathPointPosition();
        }
    }

    // Make a small random offset to a copy of pathpointposition to make units move more "live"
    private void SetOffsetPathPointPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * _positionOffset;

        _currentPathpointPosition = new Vector3 (_currentPathpoint.position.x + randomOffset.x, _currentPathpoint.position.y + randomOffset.y, _currentPathpoint.position.z);
    }

    // Setting required path (need to be in start)
    private void SetPath()
    {
        _path = PathsManager.Instance.GetPath(_requestedPathType);
    }

    // Setting current pathpoint to move
    private void UpdatePathPoint()
    {
        // if last point - do logic
        if (IsPathEnd())
        {
            // event to dmg player
            Destroy(gameObject);
            return;
        }

        _currentPathpoint = _path.GetPathPoints()[_pathpointIndex];
    }

    // Setting next pathpoint to move
    private void SetNextPoint()
    {
        _pathpointIndex++;   
    }

    // check if is it end of the path
    private bool IsPathEnd()
    {
        return _pathpointIndex == _path.GetPathPoints().Length;
    }
}
