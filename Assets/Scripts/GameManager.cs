using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isGameStarted = false;
    [HideInInspector] public bool isGameOver = false;

    private static GameManager instance;
    private int score = 0;
    private int highScore = 0;
    private int diamond = 0;

    public int Score => score;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
                if (instance == null)
                {
                    Debug.LogError("GameManager not found");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UIManager.Instance.OpenMainMenu();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UIManager.Instance.UpdateHighScore(highScore);

        diamond = PlayerPrefs.GetInt("Diamonds", 0);
        UIManager.Instance.UpdateDiamondsUI(diamond);

    }

    public void StartGame()
    {
        isGameStarted = true;
        isGameOver = false;
        UIManager.Instance.CloseMainMenu();
    }

    public void RestartGame()
    {
        isGameStarted = false;
        UIManager.Instance.OpenGameOverMenu(score, highScore);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        isGameOver = true;
        StartCoroutine(WaitAndShowGameOver());
    }

    public void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            UIManager.Instance.UpdateHighScore(highScore);

            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UIManager.Instance.UpdateScoreUI(score);
    }

    public void AddDiamond()
    {
        diamond++;

        int currentTotalDiamonds = PlayerPrefs.GetInt("Diamonds", 0);

        currentTotalDiamonds += 1;

        PlayerPrefs.SetInt("Diamonds", currentTotalDiamonds);
        PlayerPrefs.Save();

        UIManager.Instance.UpdateDiamondsUI(diamond);
    }



    IEnumerator WaitAndShowGameOver()
    {
        yield return new WaitForSeconds(1f);
        ShowGameOverMenu();
    }

    private void ShowGameOverMenu()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        UIManager.Instance.OpenGameOverMenu(score, highScore);

    }

}
