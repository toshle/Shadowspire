using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && (GameManager.Instance.State == GameState.HubLevel || GameManager.Instance.State == GameState.ArenaLevel))
        {
            Resume();
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        GameManager.Instance.TogglePaused();
        Destroy(gameObject);
    }

    public void MainMenu()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        Destroy(gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
