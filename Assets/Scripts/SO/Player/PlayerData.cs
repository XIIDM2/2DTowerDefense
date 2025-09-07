using UnityEngine;

// player info
[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/Player/PlayerData")]
public class PlayerData : ScriptableObject, IHealthConfig
{
    [SerializeField] private int baseHealth;
    [SerializeField] private int baseGold;
    [SerializeField] private int baseEnergy;
    [SerializeField] private int baseEnergyGain;

    public int BaseHealth => baseHealth;
    public int BaseGold => baseGold;
    public int BaseEnergy => baseEnergy;
    public int BaseEnergyGain => baseEnergyGain;
}
