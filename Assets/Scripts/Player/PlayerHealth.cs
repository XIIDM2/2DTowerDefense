using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerData _playerData;

    protected override void Start()
    {
        _maxHealth = _playerData.StartHealth;
        base.Start();
    }

    public void SetPlayerData(PlayerData playerData)
    {
        _playerData = playerData;
    }

    protected override void HealthChangedEventInvoke()
    {
        // Messenger.Broadcast(GameEvents.PlayerDamaged); add when add gui
    }

    protected override void DeathEventInvoke()
    {
        Messenger.Broadcast(GameEvents.PlayerDead);
    }
}
