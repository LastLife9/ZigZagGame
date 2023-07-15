using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnGamePause { get; set; }
    public static Action OnGameUnause { get; set; }

    private void OnEnable()
    {
        OnGamePause += GamePause;
        OnGameUnause += GameUnpause;
    }
    private void OnDisable()
    {
        OnGamePause -= GamePause;
        OnGameUnause -= GameUnpause;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void GamePause()
    {
        Time.timeScale = 0;
    }

    private void GameUnpause()
    {
        Time.timeScale = 1;
    }

    [ContextMenu("Delete Save")]
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
