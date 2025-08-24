using UnityEngine;

// Singleton for player instance
public class PlayerManager : Singleton<PlayerManager>
{
    public Health Health => _health;

    private Health _health;

    protected override void Awake()
    {
        base.Awake();

        _health = GetComponent<Health>();
    }

}
