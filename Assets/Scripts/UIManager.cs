using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu Assets")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private List<GameObject> mainMenuButtons;
    [SerializeField] private TextMeshProUGUI mainMenuighScoreText;

    [Header("Shop Assets")]
    [SerializeField] private GameObject shopMenuPanel;
    [SerializeField] private List<GameObject> shopMenuButtons;

    [Header("Game Over Assets")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private List<GameObject> gameOverButtons;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreText;

    [Header("Text For Blink")]
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [Header("Mange Text Blink Speed")]
    [Range(0f, 19f)]
    [SerializeField] private float speed = 1f;

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Diamonds Text")]
    [SerializeField] private TextMeshProUGUI diamondText;
    //UIManager SingleTon Pattern

    private string[] suffixes = { "", "K", "M", "B", "T", "A", "B", "C", "D", "E" };

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<UIManager>();
                if (instance == null)
                {
                    Debug.LogError("UIManager not found");
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
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

  
    public void StartSetup()
    {
        if (GameManager.Instance.isGameStarted)
        {
            scoreText.gameObject.SetActive(true);
            diamondText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        AnimateTextBlink(textMeshProUGUI);
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.transform.localScale = Vector3.zero;
        mainMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        mainMenuPanel.SetActive(true);
        int highSore = PlayerPrefs.GetInt("HighScore", 0);
        mainMenuighScoreText.text = "Best Score: " + highSore;
    }

    public void CloseMainMenu()
    {
        mainMenuPanel.transform.localScale = Vector3.zero;
        mainMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        mainMenuPanel.SetActive(false);
    }

    public void OpenGameOverMenu(int finalScore, int highScore)
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.one;
        gameOverPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        finalScoreText.text = "Score: " + finalScore;
        gameOverHighScoreText.text = "Best Score: " + highScore;
    }

    public void CloseGameOverMenu()
    {
        gameOverPanel.transform.localScale = Vector3.one;
        gameOverPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        gameOverPanel.SetActive(false);
    }

    public void OpenShopMenu()
    {
        CloseMainMenu();
        shopMenuPanel.transform.localScale = Vector3.zero;
        shopMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        shopMenuPanel.SetActive(true);      // Shop dikhao
        AnimateButtons(shopMenuButtons);
    }
    public void CloseShopMenu()
    {
        shopMenuPanel.SetActive(false);
        OpenMainMenu();
        AnimateButtons(mainMenuButtons);
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + FormatAdvanceScore(score);
    }

    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = "Best: " + FormatAdvanceScore(highScore);
    }

    public void UpdateDiamondsUI(int diamond)
    {
        diamondText.text = "Diamonds: " + FormatAdvanceScore(diamond);
    }

    string FormatAdvanceScore(double score)
    {
        if (score < 1000) return score.ToString("0");

        int exp = (int)(Mathf.Log((float)score, 1000));

        // Agar suffixes ki limit se bahar nikal jaye toh aakhri wala uthao
        if (exp >= suffixes.Length) exp = suffixes.Length - 1;

        double number = score / Mathf.Pow(1000, exp);

        // Result: 1.5A ya 10.2B
        return string.Format("{0:F1}{1}", number, suffixes[exp]);
    }

    void AnimateTextBlink(TextMeshProUGUI tMPro)
    {
        float alpha = Mathf.PingPong(Time.time * speed, 1.0f);

        // Text ka color wahi rahega, sirf Alpha (transparency) badlegi
        Color newColor = tMPro.color;
        newColor.a = alpha;
        tMPro.color = newColor;
    }
    void AnimateButtons(List<GameObject> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.localScale = Vector3.zero;
            buttons[i].transform.DOScale(Vector3.one, 0.4f)
                .SetDelay(i * 0.1f)
                .SetEase(Ease.OutBack);
        }
    }
}
