using UnityEngine;
using VContainer;

public class UnitAttack : MonoBehaviour
{
    private int _playerDamage;

    private UnitPath _path;

    [Inject] private readonly UnitData _UnitData;

    private void Awake()
    {
        _path = GetComponent<UnitPath>();
    }


    // Set values from scriptableobject
    private void Start()
    {
        _playerDamage = _UnitData.PlayerDamage;
    }

    private void OnEnable()
    {
        _path.OnPathEnd += ApplyDamageToPlayer;
    }

    private void OnDisable()
    {
        _path.OnPathEnd -= ApplyDamageToPlayer;
    }

    // Apply damage to player if we finished path (sign to event from path)
    private void ApplyDamageToPlayer()
    {
        PlayerManager.Instance.Health.TakeDamage(_playerDamage);
        Destroy(gameObject);
    }
}
