using UnityEngine;

public class Stats : MonoBehaviour
{
    public float MovementSpeed = 0f; //Done
    public float Armor = 0f;
    public float BonusDamage = 0f; //Done
    public float AttackSpread = 0f;
    public float AttackRange = 0f;
    public int AttackPassThrough = 0; //Done
    public float AttackSpeed = 0f; //Done
    public float LifeSteal = 0f;
    public float HealthRegeneration = 0f;
    public bool HealOnLevelUp = false; //Done
    public bool ExplodingProjectiles = false;


    // movement speed, attack speed, attack pass through, health, armor, damage bonus, attack spread, attack range
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Copy(Stats stats)
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
