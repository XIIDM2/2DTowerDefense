using UnityEngine;

// Singleton for player instance
public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerHealth Health => _health;

    private PlayerHealth _health;

    protected override void Awake()
    {
        base.Awake();

        _health = GetComponent<PlayerHealth>();
    }

}
