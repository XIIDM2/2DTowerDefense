using UnityEngine;

// Unit Data
[CreateAssetMenu(fileName = "Unit Data", menuName = "ScriptableObject/Unit/UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int playerDamage;
    [SerializeField] private int unitDamage;

    public int MaxHealth => maxHealth;
    public float MovementSpeed => movementSpeed;
    public int PlayerDamage => playerDamage;
    public int UnitDamage => unitDamage;

}
