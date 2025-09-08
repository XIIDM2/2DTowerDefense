using UnityEngine;

[CreateAssetMenu(fileName = "Unit Stats", menuName = "ScriptableObjects/Unit/UnitStats")]
public class UnitStats : ScriptableObject
{
    [SerializeField] private int baseHealth;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int damageToPlayer;
    [SerializeField] private int damageToUnit;

    public int BaseHealth => baseHealth;
    public float MovementSpeed => movementSpeed;
    public int DamageToPlayer => damageToPlayer;
    public int DamageToUnit => damageToUnit;
}
