using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _canvasesContainer;
    [SerializeField] private int _width, _height, _wallDensity;
    [SerializeField] private Transform _levelTransform;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Transform _playerTransform;

    private bool[][] _grid;
    private GameObject[][] _map;
    private bool _playerSpawnPlaced = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grid = new bool[_width][];
        _map = new GameObject[_width][];
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLevel()
    {
        for (var x = 0; x < _width; x++)
        {
            _grid[x] = new bool[_height];
            _map[x] = new GameObject[_height];
            for (var z = 0; z < _height; z++)
            {
                float height = -0.6f;
                if (Mathf.Floor((UnityEngine.Random.value * 100) + 1) > _wallDensity)
                {
                    height = 0.5f;
                    _grid[x][z] = true;
                    var spawnedTile = Instantiate(_wallPrefab, _levelTransform);
                    spawnedTile.transform.position = _levelTransform.position + new Vector3(x * 5, height, z * 5);
                    spawnedTile.name = $"Wall {x}, {z}";
                    _map[x][z] = spawnedTile;
                } else
                {
                    if (!_playerSpawnPlaced && Mathf.Floor((UnityEngine.Random.value * 100) + 1) > 95)
                    {
                        _playerSpawnPlaced = true;
                        _playerTransform.position = _levelTransform.position + new Vector3(x * 5, 0, z * 5);
                    }
                }
            }
        }

        for (var x = 0; x < _width; x++)
        {
            for (var z = 0; z < _height; z++)
            {
                if(isIsolated(x,z))
                {
                    Destroy(_map[x][z]);
                    _grid[x][z] = false;
                    //_map[x][z].transform.position = new Vector3(_map[x][z].transform.position.x, -0.6f, _map[x][z].transform.position.z) ;
                }
            }
        }
    }

    private bool isIsolated(int x, int y)
    {
        bool isIsolated = false;
        int isolationScore = 0;
        if (_grid[x][y])
        {
            if (x > 0 && !_grid[x - 1][y])
                isolationScore++;
            if (y > 0 && !_grid[x][y -1])
                isolationScore++;
            if (x < _width - 1 && !_grid[x + 1][y])
                isolationScore++;
            if (y < _height - 1 && !_grid[x][y+1])
                isolationScore++;
            if(isolationScore >= 3)
            {
                isIsolated = true;
            }
        }
        return isIsolated;
    }
}
