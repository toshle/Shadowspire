using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public float MovementSpeed = 0f;
    public float Armor = 0f;
    public float BonusDamage = 0f;
    public float AttackSpread = 0f;
    public float AttackRange = 0f;
    public int AttackPassThrough = 0;
    public float AttackSpeed = 0f;
    public float LifeSteal = 0f;
    public int HealthRegeneration = 0;
    public bool HealOnLevelUp = false;
    public bool ExplodingProjectiles = false;

    public string UpgradeText = string.Empty;
}
