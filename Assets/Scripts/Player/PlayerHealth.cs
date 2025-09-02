// player health script with its own behavior
public class PlayerHealth : Health
{
    private PlayerData _playerData;

    protected override void Start()
    {
        _maxHealth = _playerData.StartHealth;
        base.Start();
    }

    // set player data (by player manager)
    public void SetPlayerData(PlayerData playerData)
    {
        _playerData = playerData;
    }

    // we invoke global events for ui when player is damaged
    protected override void HealthChangedEventInvoke()
    {
        // Messenger.Broadcast(GameEvents.PlayerDamaged); add when add gui
    }

    // we invoke defeat state by scene controller when player is dead
    protected override void DeathEventInvoke()
    {
        Messenger.Broadcast(GameEvents.PlayerDead);
    }
}
