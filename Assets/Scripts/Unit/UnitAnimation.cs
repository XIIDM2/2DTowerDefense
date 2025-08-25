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
        Vector2 Velocity = _movement.GetUnitVelocity();

        _animator.SetFloat("Horizontal", Velocity.x);
        _animator.SetFloat("Vertical", Velocity.y);
        _animator.SetFloat("MovementSpeed", Velocity.magnitude);
    }
}
