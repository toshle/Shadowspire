using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private GameObject _continueButton, _restartButton, _toHubButton;
    public bool HasWon = false;

    private void Start()
    {
        Time.timeScale = 0;
        if(HasWon)
        {
            _title.text = "YOU WIN";
            _restartButton.SetActive(false);
        } else
        {
            _title.text = "YOU DIED";
            _continueButton.SetActive(false);
        }
    }

    private void Update()
    {
    }

    public void Continue()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.UpdateGameState(GameState.ArenaLevel);
        //Add progress transfer
        Destroy(gameObject);
    }

    public void ToHub()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.UpdateGameState(GameState.HubLevel);
        Destroy(gameObject);
    }

    public void Restart()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.UpdateGameState(GameState.ArenaLevel);
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
