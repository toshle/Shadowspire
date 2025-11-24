using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.UpdateGameState(GameState.HubLevel);
        Destroy(gameObject);
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
