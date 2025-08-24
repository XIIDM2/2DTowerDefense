using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed; // make from scriptableobject later
    [SerializeField] private int _damageToPlayer;

    private Vector2 _direction;
    private float _velocity;

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
        MoveToCurrentPoint();

        // Unit direction vector and velocity
        SetUnitVelocityAndDirection();

        // Flip Horizontal unit direction to face movement direction correctly 
        ChangeUnitHorizontallScale();

        // Going for next position if close to current position
        MoveToNextPoint();
    }

    // Return unit direction
    public Vector2 GetUnitDirection()
    {
        return _direction;
    }

    // Return unit velocity
    public float GetUnitVelocity()
    {
        return _velocity;
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
            PlayerManager.Instance.Health.TakeDamage(_damageToPlayer);

            Destroy(gameObject);
            return;
        }

        _currentPathpoint = _path.GetPathPoints()[_pathpointIndex];
    }

    private void SetUnitVelocityAndDirection()
    {
        Vector2 delta = _currentPathpoint.position - transform.position;
        _direction = delta.normalized;
        _velocity = Mathf.Min(delta.magnitude / Time.deltaTime, _movementSpeed);
    }  
    
    private void MoveToCurrentPoint()
    {
        float step = _movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _currentPathpoint.position, step);
    }

    private void MoveToNextPoint()
    {
        if (Vector2.Distance(transform.position, _currentPathpoint.position) < _distanceToNextPoint)
        {
            _pathpointIndex++;
            SetCurrentPosition();
        }
    }

    // check if is it end of the path
    private bool IsPathEnd()
    {
        return _pathpointIndex == _path.GetPathPoints().Length;
    }

    private void ChangeUnitHorizontallScale()
    {
        Vector3 transformLocalScale = transform.localScale;

        if (_direction.x > 0.01f)
        {
            transformLocalScale.x = -Mathf.Abs(transformLocalScale.x);
        }
        else if (_direction.x < 0.01f)
        {
            transformLocalScale.x = Mathf.Abs(transformLocalScale.x);
        }

        transform.localScale = transformLocalScale;
    }
}
