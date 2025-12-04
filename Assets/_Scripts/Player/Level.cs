using UnityEngine;

public class Level : MonoBehaviour
{
    public int CurrentExp = 0;
    public int CurrentLevel = 1;
    private int LevelUpStep = 100;
    public void GiveExp(int xp) {
        CurrentExp += xp;
        if (CurrentExp >= LevelUpStep * CurrentLevel) {
            CurrentLevel += 1;
            CurrentExp = 0;
        }
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
