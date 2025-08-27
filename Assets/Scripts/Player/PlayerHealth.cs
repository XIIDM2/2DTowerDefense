public class PlayerHealth : Health
{
    protected override void HealthChangedEventInvoke()
    {
        Messenger.Broadcast(GameEvents.PlayerDamaged);
    }

    protected override void DeathEventInvoke()
    {
        Messenger.Broadcast(GameEvents.PlayerDead);
    }
}
