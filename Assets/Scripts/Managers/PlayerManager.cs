using UnityEngine;

// Singleton for player instance, which controll player`s resourses
public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerHealth Health => _health;

    [SerializeField] private PlayerData _data;

    private int _gold = 30;

    [Header("Player Energy")]
    private int _energy;
    private int _energyGain = 1;
    private float _energyGainInterval = 1;

    private float _lastEnergyUpdate = float.MinValue;

    private PlayerHealth _health;

    protected override void Awake()
    {
        base.Awake();

        _health = GetComponent<PlayerHealth>();

        // init player health
        _health.SetPlayerData(_data);
    }

    // set player`s stats
    private void Start()
    {
        _gold = _data.StartGold;
        _energy = _data.StartEnergy;
        _energyGain = _data.EnergyGain;

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

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 540, 100, 50), "Kill All Units"))
        {
            var units = FindObjectsByType<UnitHealth>(FindObjectsSortMode.None);

            foreach (var unit in units)
            {
                unit.TakeDamage(100);
            }
        }

        GUI.Box(new Rect(10, 10, 180, 45), "Stats");

        GUI.Label(new Rect(20, 30, 50, 30), $"Gold: {_gold}");

        GUI.Box(new Rect(1720, 10, 180, 45), $"Health: {_health.GetCurrentHealth()}");

        GUI.Label(new Rect(80, 30, 80, 40), $"Energy: {_energy}");

        Debug.Log("Player Stats box " + gameObject.name);

        Debug.Log("Test button to kill all units at " + gameObject.name);
    }
}
