using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Upgrade> _upgrades;
    [SerializeField] GameObject _upgrade1, _upgrade2, _upgrade3;
    [SerializeField] public Level PlayerLevel;
    [SerializeField] List<Upgrade> _choices;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Choose 3 random upgrades
        _choices = _upgrades.OrderBy(i => Guid.NewGuid()).Take<Upgrade>(3).ToList();
        // Update buttons
        var upgrade1Text = _upgrade1.GetComponentInChildren<TextMeshProUGUI>();
        upgrade1Text.text = _choices[0].UpgradeText;
        var upgrade2Text = _upgrade2.GetComponentInChildren<TextMeshProUGUI>();
        upgrade2Text.text = _choices[1].UpgradeText;
        var upgrade3Text = _upgrade3.GetComponentInChildren<TextMeshProUGUI>();
        upgrade3Text.text = _choices[2].UpgradeText;
    }
    
    public void Click1Upgrade()
    {
        PlayerLevel.ApplyUpgrade(_choices[0]);
        Time.timeScale = 1;
        GameManager.Instance.IsPaused = false;
        Destroy(gameObject);
    }
    public void Click2Upgrade()
    {
        PlayerLevel.ApplyUpgrade(_choices[1]);
        Time.timeScale = 1;
        GameManager.Instance.IsPaused = false;
        Destroy(gameObject);
    }

    public void Click3Upgrade()
    {
        PlayerLevel.ApplyUpgrade(_choices[2]);
        Time.timeScale = 1;
        GameManager.Instance.IsPaused = false;
        Destroy(gameObject);
    }
}
