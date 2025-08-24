using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed; // make from scriptableobject later

    [Header("Path Settings")]
    [SerializeField] private PathTypes _requestedPathType;
    [SerializeField] private float _distanceToNextPoint = 0.5f; // 0.5f is base value

    private Path _path;
    private int _pathpointIndex = 0;
    private Transform _currentPathpoint;

    // Setting various options for unit
    private void Start()
    {
        SetPath();
        SetCurrentPosition();
    }

    
    private void Update()
    {
        // Moving unit to pathpoint
        float step = _movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _currentPathpoint.position, step);

        // Going for next position if close to current position
        if (Vector2.Distance(transform.position, _currentPathpoint.position) < _distanceToNextPoint)
        {
            _pathpointIndex++;
            SetCurrentPosition();
        }
    }

    // Setting required path (need to be in start)
    private void SetPath()
    {
        _path = PathsManager.Instance.GetPath(_requestedPathType);
    }

    // Setting current position to move
    private void SetCurrentPosition()
    {
        // if last point - do logic
        if (IsPathEnd())
        {
            Destroy(gameObject);
            return;
        }

        _currentPathpoint = _path.GetPathPoints()[_pathpointIndex];
    }

    // check if is it end of the path
    private bool IsPathEnd()
    {
        return _pathpointIndex == _path.GetPathPoints().Length;
    }
}
