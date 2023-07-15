using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _gamePanel;
    [SerializeField] 
    private GameObject _menuPanel;
    [SerializeField] 
    private GameObject _fallPanel;
    [SerializeField] 
    private GameObject _pausePanel;
    [SerializeField] 
    private Toggle _soundToggle;

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

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Sound", _soundToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("Sound", _soundToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Sound"))
            _soundToggle.isOn = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
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
    public void OnSoundState(bool state)
    {
        SoundManager.OnChangeState?.Invoke(state);
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
