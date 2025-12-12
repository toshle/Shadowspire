using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] List<Upgrade> _upgrades;
    [SerializeField] GameObject _upgrade1, _upgrade2, _upgrade3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Choose 3 random upgrades
        var choices = _upgrades.OrderBy(i => Guid.NewGuid()).Take<Upgrade>(3).ToList();
        // Update buttons
        var upgrade1Text = _upgrade1.GetComponentInChildren<TextMeshProUGUI>();
        upgrade1Text.text = choices[0].UpgradeText;
        var upgrade2Text = _upgrade2.GetComponentInChildren<TextMeshProUGUI>();
        upgrade2Text.text = choices[1].UpgradeText;
        var upgrade3Text = _upgrade3.GetComponentInChildren<TextMeshProUGUI>();
        upgrade3Text.text = choices[2].UpgradeText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
