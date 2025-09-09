using UnityEngine;
using VContainer;

// Singleton for player instance, which control player`s resourses
public class PlayerManager : MonoBehaviour, IPlayerController
{
    public Health Health => _health;

    private int _gold;

    [Header("Player Energy")]
    private int _energy;
    private int _energyGain;
    private float _energyGainInterval = 1;

    private float _lastEnergyUpdate = float.MinValue;

    [Inject] private PlayerData _data;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    // set player`s stats
    private void Start()
    {
        _health.Init(_data.BaseHealth);
        _gold = _data.BaseGold;
        _energy = _data.BaseEnergy;
        _energyGain = _data.BaseEnergyGain;

    }

    private void OnEnable()
    {
         Messenger<int>.AddListener(GameEvents.PlayerDamaged, ApplyDamageToPlayer);
        _health.OnDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
         Messenger<int>.RemoveListener(GameEvents.PlayerDamaged, ApplyDamageToPlayer);
        _health.OnDeath -= OnPlayerDeath;
    }

    private void Update()
    {
        // increase energy each after each interval
        if (Time.time > _lastEnergyUpdate + _energyGainInterval)
        {
            _energy += _energyGain;
            _lastEnergyUpdate = Time.time;
        }
    }
    private void ApplyDamageToPlayer(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnPlayerDeath()
    {
        Messenger.Broadcast(GameEvents.PlayerDead);
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 180, 45), "Stats");

        GUI.Label(new Rect(20, 30, 50, 30), $"Gold: {_gold}");

        GUI.Box(new Rect(1720, 10, 180, 45), $"Health: {_health.GetCurrentHealth()}");

        GUI.Label(new Rect(80, 30, 80, 40), $"Energy: {_energy}");

        Debug.Log("Player Stats box " + gameObject.name);

        Debug.Log("Test button to kill all units at " + gameObject.name);
    }
}
