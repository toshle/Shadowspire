using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private GameObject _xpBar;
    [SerializeField] private TextMeshProUGUI _xpText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _killsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float current, float max)
    {
        float barScale = current / max;
        string text = current + "/" + max;
        _healthBar.transform.localScale = new Vector3(barScale, 1, 1);
        _healthText.text = text;
    }

    public void SetXP(float current, float max)
    {
        float barScale = current / max;
        string text = current + "/" + max;
        _xpBar.transform.localScale = new Vector3(barScale, 1, 1);
        _xpText.text = text;
    }

    public void SetLevel(float value)
    {
        string text = "Level " + value;
        _levelText.text = text;
    }

    public void SetKills(float value)
    {
        string text = value + " Kills";
        _killsText.text = text;
    }
}
