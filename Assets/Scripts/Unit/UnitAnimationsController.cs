using System.Collections;
using UnityEngine;

public class UnitAnimationsController : MonoBehaviour
{
    private const string DISOLVE_STATE_NAME = "Disolve";

    [SerializeField] private float _timeToDisolve = 3.0f; // standart is 3.0f;

    private UnitMovement _movement;
    private Health _health;

    private Animator _animator;

    private void Awake()
    {
        _movement = transform.root.GetComponent<UnitMovement>();
        _health = transform.root.GetComponent<Health>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // todo to make override controllers and clips
    }

    private void OnEnable()
    {
        _health.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        _health.OnDeath -= OnDeath;
    }

    // animate units
    private void Update()
    {
        Vector2 Velocity = _movement.GetVelocity();

        _animator.SetFloat("Horizontal", Velocity.x);
        _animator.SetFloat("Vertical", Velocity.y);
        _animator.SetFloat("MovementSpeed", Velocity.magnitude);
    }

    // change animation state to death state, if unit has dissolve animation start disolve routine)
    private void OnDeath()
    {
        _animator.SetBool("IsDead", true);


        if (_animator.HasState(0, Animator.StringToHash(DISOLVE_STATE_NAME)))
        {
            StartCoroutine(DisolveRoutine());
        }
    }

    /// change animation state to disolve state after time
    private IEnumerator DisolveRoutine()
    {
        yield return new WaitForSeconds(_timeToDisolve);

        _animator.SetBool("IsDisolve", true);

    }

    /// <summary>
    /// Special method for animation event to destroy object after death/disolve animation (FOR ANIMATION EVENT ONLY)
    /// </summary>
    private void AEDestroyGameObject()
    {
        Destroy(transform.root.gameObject);
    }
}
