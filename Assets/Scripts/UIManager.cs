using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu Assets")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private List<GameObject> mainMenuButtons;

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

    void Start()
    {
        StartSetup();
    }

    void StartSetup()
    {
        scoreText.gameObject.SetActive(true);
        diamondText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
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
        finalScoreText.text = "Score: " + highScore.ToString();
        finalScoreText.text = "Best: " + finalScore.ToString();
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
        scoreText.text = "Score: " + score;
    }

    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = "Best: " + highScore;
    }

    public void UpdateDiamondsUI(int diamond)
    {
        diamondText.text = "Diamonds: " + diamond;
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
