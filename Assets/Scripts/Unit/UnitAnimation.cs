using System.Collections;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private const string DISOLVE_STATE_NAME = "Disolve";

    private UnitMovement _movement;
    private UnitHealth _health;

    private Animator _animator;

    private void Awake()
    {
        _movement = transform.root.GetComponent<UnitMovement>();
        _health = transform.root.GetComponent<UnitHealth>();
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        OnDeath();
    }
    private void OnEnable()
    {
        _health.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        _health.OnDeath -= OnDeath;
    }


    private void Update()
    {
        Vector2 Velocity = _movement.GetVelocity();

        _animator.SetFloat("Horizontal", Velocity.x);
        _animator.SetFloat("Vertical", Velocity.y);
        _animator.SetFloat("MovementSpeed", Velocity.magnitude);
    }

    private void OnDeath()
    {
        _animator.SetBool("IsDead", true);


        if (_animator.HasState(0, Animator.StringToHash(DISOLVE_STATE_NAME)))
        {
            StartCoroutine(DisolveRoutine());
        }
    }

    private IEnumerator DisolveRoutine()
    {
        yield return new WaitForSeconds(3);

        _animator.SetBool("IsDisolve", true);

    }
}
