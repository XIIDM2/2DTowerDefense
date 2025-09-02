using UnityEngine;

// player info
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int startHealth;
    [SerializeField] private int startGold;
    [SerializeField] private int startEnergy;
    [SerializeField] private int energyGain;

    public int StartHealth => startHealth;
    public int StartGold => startGold;
    public int StartEnergy => startEnergy;
    public int EnergyGain => energyGain;
}
