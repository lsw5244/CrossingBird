using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public Text scoreText;
    private int _score = 0;

    public Text highScoreText;
    //private int _highScore;

    public Canvas gameOverCanvas;

    void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 UI 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = _score.ToString();

        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void UpdateScore()
    {
        ++_score;
        if(_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            UpdateHighScore();
        }
        scoreText.text = _score.ToString();
    }

    void UpdateHighScore()
    {
        PlayerPrefs.SetInt("HighScore", _score);
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void GameOver()
    {
        gameOverCanvas.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
