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
    public float HealthRegeneration = 0;
    public bool HealOnLevelUp = false;
    public bool ExplodingProjectiles = false;
    public bool PoisonProjectiles = false;

    public string UpgradeText = string.Empty;
    public int UseLimit = 0;
    public int TimesUsed = 0;

    public void CopyStats(Stats stats)
    {
        MovementSpeed = stats.MovementSpeed;
        Armor = stats.Armor;
        BonusDamage = stats.BonusDamage;
        AttackSpread = stats.AttackSpread;
        AttackRange = stats.AttackRange;
        AttackSpeed = stats.AttackSpeed;
        AttackPassThrough = stats.AttackPassThrough;
        LifeSteal = stats.LifeSteal;
        HealthRegeneration = stats.HealthRegeneration;
        HealOnLevelUp = stats.HealOnLevelUp;
        ExplodingProjectiles = stats.ExplodingProjectiles;
    }
}
