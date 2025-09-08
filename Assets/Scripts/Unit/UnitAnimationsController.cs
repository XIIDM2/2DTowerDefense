using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationsController : MonoBehaviour
{
    private const string DISOLVE_STATE_NAME = "Disolve";

    [SerializeField] private float _timeToDisolve = 3.0f; // standart is 3.0f;

    private List<KeyValuePair<AnimationClip, AnimationClip>> _overrides;

    private UnitMovement _movement;
    private Health _health;

    private Animator _animator;
    private UnitAnimations _unitAnimations;

    public void Init(UnitAnimations unitAnimations)
    {
        _unitAnimations = unitAnimations;
    }

    private void Awake()
    {
        _movement = transform.root.GetComponent<UnitMovement>();
        _health = transform.root.GetComponent<Health>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CreateOverrideController();
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

    private void CreateOverrideController()
    {
        // Creating custom overrideController for unit based on default controller
        AnimatorOverrideController overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);

        // Creating list of pairs of default clip - override clip with controller clips capacity
        _overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(overrideController.overridesCount);

        // Gets the list of Animation Clip overrides currently defined in this Animator Override Controller. (from documentation)
        overrideController.GetOverrides(_overrides);

       
        for (int i = 0; i < overrideController.overridesCount; i++)
        {
            // trying to find requested clip
            AnimationClip overrideClip = _unitAnimations.GetClip(_overrides[i].Key.name);

            if (overrideClip != null)
            {
                // setting requested clip for override controller
                _overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(_overrides[i].Key, overrideClip);
            }
            else
            {
                Debug.LogWarningFormat("Requested override clip {0} for {1} was null, if it is intended, ignore this message", _overrides[i].Key.name, gameObject.transform.root.name);
            }

        }

        // applying changes
        overrideController.ApplyOverrides(_overrides);

        // set overridecontroller in animator to play
        _animator.runtimeAnimatorController = overrideController;
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
