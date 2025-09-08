using UnityEngine;
using VContainer;

public class UnitMovement : MonoBehaviour
{
    private float _movementSpeed;

    private Vector2 _velocity;

    private UnitPath _currentPath;

    /// <summary>
    /// Script Init method for single point entry - unit controller
    /// </summary>
    /// <param name="movementSpeed"></param>
    public void Init(float movementSpeed)
    {
        _movementSpeed = movementSpeed;

        if (movementSpeed <= 0)
        {
            Debug.LogWarning($"Movement speed of {gameObject.name} <= 0. If it is not intended, check UnitMovement script");
        }
    }

    private void Awake()
    {
        _currentPath = GetComponent<UnitPath>();
    }
    
    private void Update()
    {
        // Unit direction vector and velocity
        SetVelocity();

        // Move unit to current pathpoint
        MoveToCurrentPoint();
        

        // Flip Horizontal unit direction to face movement direction correctly 
        ChangeHorizontallScale();
    }

    /// <summary>
    /// Returns Unit Velocity
    /// </summary>
    /// <returns></returns>
    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    /// <summary>
    /// Reset velocity to zero
    /// </summary>
    public void ResetVelocity()
    {
        _velocity = Vector2.zero;
    }

    // Calculate Unit Velocity (for animator mainly)
    private void SetVelocity()
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
    private void ChangeHorizontallScale()
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
