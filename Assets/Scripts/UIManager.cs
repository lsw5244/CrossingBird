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
    public Text finalScoreText;
    public Text finalHighScoreText;

    public Canvas fadeCanvas;
    public Image fadeImage;

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

        StartCoroutine(FadeIn());
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

    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        finalScoreText.text = $"Scroe : {_score}";
        finalHighScoreText.text = $"High Score : {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    public void Restart()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn() // 밝아지기
    {
        fadeCanvas.gameObject.SetActive(true);
        Color fadeColor = fadeImage.color;

        while (fadeColor.a > 0)
        {
            fadeColor.a -= 0.1f;
            fadeImage.color = fadeColor;
            yield return new WaitForSeconds(0.1f);
        }
        fadeCanvas.gameObject.SetActive(false);
    }

    IEnumerator FadeOut() // 어두워지기
    {
        fadeCanvas.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
        Color fadeColor = fadeImage.color;

        while (fadeColor.a < 1f)
        {
            fadeColor.a += 0.1f;
            fadeImage.color = fadeColor;
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(0);
    }
}
