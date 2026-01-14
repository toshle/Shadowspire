using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int CurrentExp = 0;
    public int CurrentLevel = 1;
    private int LevelUpStep = 100;
    private int _kills = 0;
    [SerializeField] private Stats _playerStats;
    [SerializeField] private Health _playerHealth;

    [SerializeField] public Dictionary<Upgrade, int> UpgradeUsage = new();

    [SerializeField] HUD _hud;
    public void GiveExp(int xp) {
        _kills++;
        CurrentExp += xp;
        if (CurrentExp >= LevelUpStep * CurrentLevel) {
            CurrentLevel += 1;
            CurrentExp = 0;
            _hud.SetLevel(CurrentLevel);
            GameManager.Instance.ShowLevelUpUpgrades();
            if (_playerStats.HealOnLevelUp)
            {
                _playerHealth.Heal(1000);
            }
        }
        _hud.SetXP(CurrentExp, LevelUpStep * CurrentLevel);
        _hud.SetKills(_kills);
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        if(UpgradeUsage.ContainsKey(upgrade))
        {
            UpgradeUsage[upgrade]++;
        } else
        {
            UpgradeUsage.Add(upgrade, 1);
        }
        _playerStats.MovementSpeed += upgrade.MovementSpeed;
        _playerStats.Armor += upgrade.Armor;
        _playerStats.BonusDamage += upgrade.BonusDamage;
        _playerStats.AttackSpeed += upgrade.AttackSpeed;
        _playerStats.AttackSpread += upgrade.AttackSpread;
        _playerStats.AttackRange += upgrade.AttackRange;
        _playerStats.AttackPassThrough += upgrade.AttackPassThrough;
        _playerStats.LifeSteal += upgrade.LifeSteal;
        _playerStats.HealthRegeneration += upgrade.HealthRegeneration;
        if(upgrade.HealOnLevelUp)
            _playerStats.HealOnLevelUp = upgrade.HealOnLevelUp;
        if (upgrade.ExplodingProjectiles)
            _playerStats.ExplodingProjectiles = upgrade.ExplodingProjectiles;
        if (upgrade.PoisonProjectiles)
            _playerStats.PoisonProjectiles = upgrade.PoisonProjectiles;
        _playerHealth.maxHealth = _playerHealth.maxHealth + upgrade.HealthBonus;
        _playerHealth.Heal(upgrade.HealthBonus);
        //upgrade.TimesUsed++;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
