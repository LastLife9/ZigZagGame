using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _fallPanel;
    [SerializeField] private GameObject _pausePanel;

    private void OnEnable()
    {
        PlayerController.OnStartMove += HideMenu;
        PlayerController.OnStartMove += ShowGamePanel;
        PlayerController.OnFallDown += HideGamePanel;
        PlayerController.OnFallDown += ShowFallPanel;
    }
    private void OnDisable()
    {
        PlayerController.OnStartMove -= HideMenu;
        PlayerController.OnStartMove -= ShowGamePanel;
        PlayerController.OnFallDown -= HideGamePanel;
        PlayerController.OnFallDown -= ShowFallPanel;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GamePause()
    {
        ShowPausePanel();
        HideGamePanel();

        GameManager.OnGamePause?.Invoke();
    }
    public void GameUnpause()
    {
        HidePausePanel();
        ShowGamePanel();

        GameManager.OnGameUnause?.Invoke();
    }
    public void OnAutopilotChange(bool state)
    {
        Autopilot.OnChangeState?.Invoke(state);
    }

    private void ShowFallPanel()
    {
        _fallPanel.SetActive(true);
    }
    private void ShowGamePanel()
    {
        _gamePanel.SetActive(true);
    }
    private void ShowPausePanel()
    {
        _pausePanel.SetActive(true);
    }
    private void HidePausePanel()
    {
        _pausePanel.SetActive(false);
    }
    private void HideGamePanel()
    {
        _gamePanel.SetActive(false);
    }
    private void HideMenu()
    {
        _menuPanel.SetActive(false);
    }
}
