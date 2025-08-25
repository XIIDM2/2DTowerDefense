using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed; // make from scriptableobject later
    [SerializeField] private int _damageToPlayer;

    private Vector2 _velocity;

    private UnitPath _currentPath;

    private void Awake()
    {
        _currentPath = GetComponent<UnitPath>();
    }

    private void Update()
    {
        // Unit direction vector and velocity
        SetUnitVelocity();

        // Move unit to current pathpoint
        MoveToCurrentPoint();
        

        // Flip Horizontal unit direction to face movement direction correctly 
        ChangeUnitHorizontallScale();
    }

    // Returns Unit Velocity
    public Vector2 GetUnitVelocity()
    {
        return _velocity;
    }

    // Calculate Unit Velocity (for animator mainly)
    private void SetUnitVelocity()
    {
        Vector2 direction = (_currentPath.PathPointPosition - transform.position).normalized;
        _velocity = direction * _movementSpeed;
    }  

    // Move unit to current pathpoint
    private void MoveToCurrentPoint()
    {
        if (_currentPath.PathPointPosition != null)
        {          
            float step = _movementSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, _currentPath.PathPointPosition, step);       
        }
        else
        {
            Debug.Log("No pathpoint to move - Unit:" + gameObject.name);
        }
    }


    // Swap Unit direction if moving in right direction
    private void ChangeUnitHorizontallScale()
    {
        Vector3 transformLocalScale = transform.localScale;

        if (_velocity.x > 0.01f)
        {
            transformLocalScale.x = -Mathf.Abs(transformLocalScale.x);
        }
        else if (_velocity.x < 0.01f)
        {
            transformLocalScale.x = Mathf.Abs(transformLocalScale.x);
        }

        transform.localScale = transformLocalScale;
    }
}
