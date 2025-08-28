using UnityEngine;
using VContainer;

public class UnitAttack : MonoBehaviour
{
    private int _playerDamage;

    [Inject] private readonly UnitData _UnitData;

    // Set values from scriptableobject
    private void Start()
    {
        _playerDamage = _UnitData.PlayerDamage;
    }

    // Apply damage to player if we finished path (sign to event from path)
    public void ApplyDamageToPlayer()
    {
        PlayerManager.Instance.Health.TakeDamage(_playerDamage);
        Destroy(gameObject);
    }
}
