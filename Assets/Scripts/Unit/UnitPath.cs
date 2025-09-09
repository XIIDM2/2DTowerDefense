using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class UnitPath : MonoBehaviour
{
    public event UnityAction OnPathEnd;
    public Vector3 PathPointPosition => _currentMovePosition;

    [field: Tooltip("Current Unit`s path (this variable is for debug purpose only and does not affect Unit`s path")]
    [field: SerializeField] public PathType RequestedPathType { get; private set; }

    [SerializeField] private float _distanceToNextPoint = 0.5f; // 0.5f is base value

    [Inject] private PathsManager _pathsManager;

    /// <summary>
    ///  Value to offset pathpoint position to make units move a little random so game feels "live"
    /// </summary>
    [SerializeField] private float _positionOffset = 0.5f; // 0.5f is base value

    private Path _path;
    private int _pathpointIndex = 0;
    private Transform _currentPathpoint;
    private Vector3 _currentMovePosition; // Saving copy of currentPathpoint position to avoid shuffling path, insted changing copy 

    // Setting path and first pathpoint
    private void Start()
    {
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

    /// <summary>
    ///  Setting required path and pathpoint position
    /// </summary>
    /// <param name="requestedPathType"></param>
    /// <param name="pathPointIndex"></param>
    public void SetPath(PathType requestedPathType, int pathPointIndex)
    {
        _path = _pathsManager.GetPath(requestedPathType);
        _pathpointIndex = pathPointIndex;
        RequestedPathType = requestedPathType;
    }

    /// <summary>
    ///  Returns current pathType and pathpointposition
    /// </summary>
    /// <returns></returns>
    public (PathType, int) GetPathData()
    {
        return (RequestedPathType, _pathpointIndex);
    }

    // Make a small random offset to a copy of pathpointposition to make units move more "live"
    private void SetOffsetPathPointPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * _positionOffset;

        _currentMovePosition = new Vector3 (_currentPathpoint.position.x + randomOffset.x, _currentPathpoint.position.y + randomOffset.y, _currentPathpoint.position.z);
    }


    // Setting current pathpoint to move
    private void UpdatePathPoint()
    {
        // if last point - do logic
        if (IsPathEnd())
        {
            OnPathEnd?.Invoke();
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
