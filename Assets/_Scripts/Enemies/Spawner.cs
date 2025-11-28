using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyAI EnemyPrefab;
    public Generator Level;

    public int MaxAlive = 50;
    public float SpawnInterval = 0.5f;
    // Max alive = 200
    // Spawn interval = 0.5f;

    private int _currentlyAlive = 0;
    private float _lastAttackTime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentlyAlive < MaxAlive)
        {
            if (Time.time - _lastAttackTime >= SpawnInterval)
            {
                int x = Mathf.FloorToInt(UnityEngine.Random.value * 100);
                int z = Mathf.FloorToInt(UnityEngine.Random.value * 100);
                if (!Level.Grid[x][z])
                {
                    _lastAttackTime = Time.time;
                    var enemy = Instantiate(EnemyPrefab, Level.transform.position + new Vector3(x * 5, 0, z * 5), Level.transform.rotation);
                    //enemy.transform.position = Level.transform.position + new Vector3(x * 5, 0, z * 5);
                    _currentlyAlive++;
                }
            }
        }
    }
}
