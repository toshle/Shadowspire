using UnityEngine;

public class Level : MonoBehaviour
{
    public int CurrentExp = 0;
    public int CurrentLevel = 1;
    private int LevelUpStep = 100;
    private int _kills = 0;

    [SerializeField] HUD _hud;
    public void GiveExp(int xp) {
        _kills++;
        CurrentExp += xp;
        if (CurrentExp >= LevelUpStep * CurrentLevel) {
            CurrentLevel += 1;
            CurrentExp = 0;
            _hud.SetLevel(CurrentLevel);
        }
        _hud.SetXP(CurrentExp, LevelUpStep * CurrentLevel);
        _hud.SetKills(_kills);
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
