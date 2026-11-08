using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuGameObject;

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

    }


    public void ResumeButton()
    {
        GameManager.Instance.TogglePauseGame();
    }
    public void MenuButton()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
    public void QuitButton()
    {
        // todo: save game
      Application.Quit();
    }

    private void GameManager_OnGameUnpaused()
    {
        Hide();
    }

    private void GameManager_OnGamePaused()
    {
        Show();
    }

    private void Hide()
    {
        pauseMenuGameObject.SetActive(false);
    }

    private void Show()
    {
        pauseMenuGameObject.SetActive(true);
    }
}
