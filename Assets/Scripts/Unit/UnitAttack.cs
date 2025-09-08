using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private int _damageToPlayer;

    /// <summary>
    /// Script Init method for single point entry - unit controller
    /// </summary>
    /// <param name="damageToPlayer"></param>
    public void Init(int damageToPlayer)
    {
        _damageToPlayer = damageToPlayer;
    }

    // Apply damage to player if we finished path (sign to event from path)
    public void ApplyDamageToPlayer()
    {
        if (_damageToPlayer <= 0)
        {
            Debug.LogWarning($"Damage to player from {gameObject.name} <= 0. If it is not intended, check UnitAttack script");
        }

        Messenger<int>.Broadcast(GameEvents.PlayerDamaged, _damageToPlayer);
        Destroy(gameObject);
    }
}
