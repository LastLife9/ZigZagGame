using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; set; }

    private const string _bestScoreKey = "best_score";
    private const string _playCountKey = "games_played";

    private int _currentScore = 0;
    private int _bestScore = 0;
    private int _playCount = 0;

    [SerializeField] 
    private TextMeshProUGUI[] _currentScoreLabel;
    [SerializeField] 
    private TextMeshProUGUI[] _bestScoreLabel;
    [SerializeField] 
    private TextMeshProUGUI[] _playedCountLabel;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey(_bestScoreKey))
        {
            _bestScore = PlayerPrefs.GetInt(_bestScoreKey);
        }
        if (PlayerPrefs.HasKey(_playCountKey))
        {
            _playCount = PlayerPrefs.GetInt(_playCountKey);
        }

        UpdateDisplay();
    }

    private void OnEnable()
    {
        PlayerController.OnStartMove += ResetCurrent;
    }
    private void OnDisable()
    {
        PlayerController.OnStartMove -= ResetCurrent;
    }

    private void ResetCurrent()
    {
        _currentScore = 0;

        UpdateDisplay();
    }

    public void IncreaseCurrentScore()
    {
        _currentScore++;
        if(_bestScore < _currentScore)
        {
            _bestScore = _currentScore;
        }

        SaveScores();
        UpdateDisplay();
    }
    public void IncreasePlayedGames()
    {
        _playCount++;

        SaveScores();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < _currentScoreLabel.Length; i++)
        {
            _currentScoreLabel[i].text = _currentScore.ToString();
        }

        for (int i = 0; i < _bestScoreLabel.Length; i++)
        {
            _bestScoreLabel[i].text = _bestScore.ToString();
        }

        for (int i = 0; i < _playedCountLabel.Length; i++)
        {
            _playedCountLabel[i].text = _playCount.ToString();
        }
    }

    private void SaveScores()
    {
        PlayerPrefs.SetInt(_bestScoreKey, _bestScore);
        PlayerPrefs.SetInt(_playCountKey, _playCount);
        PlayerPrefs.Save();
    }
}
