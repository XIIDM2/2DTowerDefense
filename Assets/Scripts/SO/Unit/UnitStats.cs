using UnityEngine;

[CreateAssetMenu(fileName = "Unit Stats", menuName = "ScriptableObjects/Unit/UnitStats")]
public class UnitStats : ScriptableObject
{
    [SerializeField] private int baseHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int playerDamage;
    [SerializeField] private int unitDamage;

    public int BaseHealth => baseHealth;
    public float MovementSpeed => movementSpeed;
    public int PlayerDamage => playerDamage;
    public int UnitDamage => unitDamage;
}
