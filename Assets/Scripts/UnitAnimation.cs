using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private UnitMovement _movement;

    private Animator _animator;

    private void Awake()
    {
        _movement = transform.root.GetComponent<UnitMovement>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Vector2 direction = _movement.GetUnitDirection();
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
        _animator.SetFloat("MovementSpeed", _movement.GetUnitVelocity());
    }
}
