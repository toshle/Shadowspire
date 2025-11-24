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

    [SerializeField] private GameObject _canvasesContainer;
    [SerializeField] private GameObject _loadingPrefab;
    [SerializeField] private MainMenu _mainMenuPrefab;
    [SerializeField] private PauseMenu _pauseMenuPrefab;
    [SerializeField] private CameraFollow _camera;

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
                /*if (BoardInstance != null)
                {
                    UnloadLevel();
                }
                SoundManager.Instance.PlayMenuMusic();*/
                Instantiate(_mainMenuPrefab, _canvasesContainer.transform);
                break;
            case GameState.HubLevel:
                UnloadLevel();
                await LoadLevel("Hub");
                break;
            case GameState.ArenaLevel:
                UnloadLevel();
                await LoadLevel("Arena");
                break;
            case GameState.Win:
                /*UnloadLevel();
                var win = Instantiate(_endGamePrefab, _canvasesContainer.transform);
                win.Init(Faction.Human);*/
                break;
            case GameState.Lose:
                /*UnloadLevel();
                var lose = Instantiate(_endGamePrefab, _canvasesContainer.transform);
                lose.Init(Faction.AI);*/
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void TogglePaused()
    {
        if (State == GameState.HubLevel || State == GameState.ArenaLevel)
        {
            if (_isPaused)
            {
               /* BoardInstance.gameObject.SetActive(true);
                _hudInstance.gameObject.SetActive(true);*/
                _isPaused = false;
            }
            else
            {
                /*BoardInstance.gameObject.SetActive(false);
                _hudInstance.gameObject.SetActive(false);*/
                Instantiate(_pauseMenuPrefab, _canvasesContainer.transform);
                _isPaused = true;
            }
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
