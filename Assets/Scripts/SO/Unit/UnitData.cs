using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "ScriptableObjects/Unit/UnitData")]
public class UnitData : ScriptableObject, IHealthConfig
{
    [SerializeField] private UnitType type;
    [SerializeField] private UnitStats stats;
    [SerializeField] private UnitAnimations animations;

    public UnitType Type => type;
    public UnitStats UnitStats => stats;
    public UnitAnimations UnitAnimations => animations;

    // IHealthConfig realisation. we took info from unitstats and assign base health through interface for health script
    public int BaseHealth => stats.BaseHealth;
}
