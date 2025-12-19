using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Player Player { get; private set; }

    [SerializeField] private GameObject _canvasesContainer;
    [SerializeField] private GameObject _loadingPrefab;
    [SerializeField] private MainMenu _mainMenuPrefab;
    [SerializeField] private PauseMenu _pauseMenuPrefab;
    [SerializeField] private UpgradeMenu _upgradeMenuPrefab;
    [SerializeField] private EndScreen _endScreenPrefab;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private Camera _menuCamera;

    private string _currentLevel;

    public GameState State { get; private set; }

    private bool _isPaused;
    public bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPaused && Keyboard.current.escapeKey.wasPressedThisFrame && (State == GameState.HubLevel || State == GameState.ArenaLevel))
        {
            TogglePaused();
        }
    }

    public async void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                UnloadLevel();
                _menuCamera.gameObject.SetActive(true);
                Instantiate(_mainMenuPrefab, _canvasesContainer.transform);
                break;
            case GameState.HubLevel:
                UnloadLevel();
                _menuCamera.gameObject.SetActive(false);
                // Показваме Loading екран
                await LoadLevel("Hub");
                // Скриваме Loading екран
                Player = FindFirstObjectByType<Player>();
                break;
            case GameState.ArenaLevel:
                UnloadLevel();
                await LoadLevel("Arena");
                Player = FindFirstObjectByType<Player>();
                break;
            case GameState.Win:
                ClearEnemies();
                EndScreen winScreen = Instantiate(_endScreenPrefab, _canvasesContainer.transform);
                winScreen.HasWon = true;
                break;
            case GameState.Lose:
                ClearEnemies();
                EndScreen loseScreen = Instantiate(_endScreenPrefab, _canvasesContainer.transform);
                loseScreen.HasWon = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void ClearEnemies()
    {
        Spawner spawner = FindFirstObjectByType<Spawner>();
        spawner.Clear();
    }

    public void TogglePaused()
    {
        if (State == GameState.HubLevel || State == GameState.ArenaLevel)
        {
            if (_isPaused)
            {
                _isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                Instantiate(_pauseMenuPrefab, _canvasesContainer.transform);
                _isPaused = true;
            }
        }
    }

    public void ShowLevelUpUpgrades()
    {
        if (State == GameState.ArenaLevel)
        {
            Time.timeScale = 0;
            var upgradesMenu = Instantiate(_upgradeMenuPrefab, _canvasesContainer.transform);
            upgradesMenu.PlayerLevel = FindFirstObjectByType<Level>(); ;
            _isPaused = true;
        }
    }

    private async Task LoadLevel(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            _currentLevel = sceneName;
            await Task.Yield();
        }
    }

    private void UnloadLevel()
    {
        if(_currentLevel != null)
        {
            SceneManager.UnloadSceneAsync(_currentLevel);
            _currentLevel = null;
        }
    }

}

public enum GameState
{
    MainMenu,
    HubLevel,
    ArenaLevel,
    Win,
    Lose
}
