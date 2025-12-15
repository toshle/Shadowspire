using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyAI EnemyPrefab;
    public EnemyAI HardEnemyPrefab;
    public EnemyAI BossEnemyPrefab;
    public Generator Level;

    public int MaxAlive = 50;
    public float SpawnInterval = 0.5f;
    public float HardEnemySpawnChance = 0.05f;
    public float BossSpawnTimeInMinutes = 5f;
    public float BossSpawnTime = 0f;
    // Max alive = 200
    // Spawn interval = 0.5f;

    [SerializeField]
    private int _currentlyAlive = 0;
    [SerializeField]
    private List<EnemyAI> _enemies = new();
    private float _lastSpawnTime;
    private float _spawnStartTime;
    private bool _bossSpawned = false;
    private EnemyAI _boss;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnStartTime = Time.time;
        BossSpawnTime = BossSpawnTimeInMinutes * 60f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time - _spawnStartTime + " >= " + BossSpawnTime + " " + (Time.time - _spawnStartTime >= BossSpawnTime));
        if (!_bossSpawned && Time.time - _spawnStartTime >= BossSpawnTime)
        {
            int x = Mathf.FloorToInt(UnityEngine.Random.value * 100);
            int z = Mathf.FloorToInt(UnityEngine.Random.value * 100);
            if (!Level.Grid[x][z])
            {
                _boss = Instantiate(BossEnemyPrefab, Level.transform.position + new Vector3(x * 5, 0, z * 5), Level.transform.rotation);
                _enemies.RemoveAll(enemy => enemy == null);
                _enemies.Add(_boss);
                _bossSpawned = true;
            }
        }

        if (_currentlyAlive < MaxAlive)
        {
            if (Time.time - _lastSpawnTime >= SpawnInterval)
            {
                int x = Mathf.FloorToInt(UnityEngine.Random.value * 100);
                int z = Mathf.FloorToInt(UnityEngine.Random.value * 100);
                float hardEnemyRoll = UnityEngine.Random.value;
                
                if (!Level.Grid[x][z])
                {
                    EnemyAI toSpawn = EnemyPrefab;
                    //Debug.Log(hardEnemyRoll);
                    if (hardEnemyRoll <= HardEnemySpawnChance)
                    {
                        toSpawn = HardEnemyPrefab;
                        //Debug.Log("SPAWNED HARD ENEMY!");
                    }
                    _lastSpawnTime = Time.time;
                    var enemy = Instantiate(toSpawn, Level.transform.position + new Vector3(x * 5, 0, z * 5), Level.transform.rotation);
                    _enemies.RemoveAll(enemy => enemy == null);
                    _enemies.Add(enemy);
                    //enemy.transform.position = Level.transform.position + new Vector3(x * 5, 0, z * 5);
                    _currentlyAlive++;
                }
            }
        }
    }

    public void KilledEnemy()
    {
        if(_boss != null && _boss.IsDead)
        {
            // Win game
        }
        _currentlyAlive--;
    }
}
