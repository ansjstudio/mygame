using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro use karne ke liye

public class ShopManager : MonoBehaviour
{
    public GameObject[] allBalls; // Inspector mein sari 3D balls drag karein
    public int currentIndex = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI diamondText; // Normal Text ki jagah TMP
    public TextMeshProUGUI buyBtnText;  // Buy button ke upar ka text
    public Button buyButton;            // Main Buy/Select Button
    public int[] ballPrices;            // Prices: 0, 50, 100...

    private int totalDiamonds;

    void Start()
    {
        totalDiamonds = PlayerPrefs.GetInt("Diamonds", 0);
        UpdateShopUI(); // Shuru mein UI set karein
    }

    public void NextBall()
    {
        currentIndex++;
        if (currentIndex >= allBalls.Length) currentIndex = 0;
        UpdateShopUI();
    }

    public void PreviousBall()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = allBalls.Length - 1;
        UpdateShopUI();
    }

    public void BuyOrSelect(BallsItem ballsItem)
    {
        if (ballsItem == null) return;

        if (ballsItem.isPurchased)
        {
            // Select Logic
            PlayerPrefs.SetInt("SelectedBall", ballsItem.id);
        }
        else
        {
            // Buy Logic
            if (totalDiamonds >= ballsItem.price)
            {
                totalDiamonds -= ballsItem.price;
                PlayerPrefs.SetInt("Diamonds", totalDiamonds);
                ballsItem.isPurchased = true;
                PlayerPrefs.SetInt("BallUnlocked_" + ballsItem.id, 1);
                PlayerPrefs.SetInt("SelectedBall", ballsItem.id);
                UIManager.Instance.UpdateDiamondsUI(totalDiamonds); // UI ko update karein
            }
            else
            {
                Debug.Log("Diamonds kam hain bhai!");
            }
        }

        //bool isUnlocked = PlayerPrefs.GetInt("BallUnlocked_" + currentIndex, 0) == 1 || currentIndex == 0;

        //if (isUnlocked)
        //{
        //    // Select Logic
        //    PlayerPrefs.SetInt("SelectedBall", currentIndex);
        //}
        //else
        //{
        //    // Buy Logic
        //    if (totalDiamonds >= ballPrices[currentIndex])
        //    {
        //        totalDiamonds -= ballPrices[currentIndex];
        //        PlayerPrefs.SetInt("Diamonds", totalDiamonds);
        //        PlayerPrefs.SetInt("BallUnlocked_" + currentIndex, 1);
        //        PlayerPrefs.SetInt("SelectedBall", currentIndex);
        //    }
        //    else
        //    {
        //        Debug.Log("Diamonds kam hain bhai!");
        //    }
        //}
        //PlayerPrefs.Save();
        //UpdateShopUI();
        //FindAnyObjectByType<PlayerController>().UpdateBallAppearance();
    }

    void UpdateShopUI()
    {
        // 1. Diamonds update karein
        diamondText.text = "Diamonds: " + totalDiamonds.ToString();

        // 2. Sirf current ball show karein
        for (int i = 0; i < allBalls.Length; i++)
        {
            allBalls[i].SetActive(i == currentIndex);
        }

        // 3. Button ka text check karein (Buy, Select, ya Selected?)
        int selectedBall = PlayerPrefs.GetInt("SelectedBall", 0);
        bool isUnlocked = PlayerPrefs.GetInt("BallUnlocked_" + currentIndex, 0) == 1 || currentIndex == 0;

        if (isUnlocked)
        {
            if (currentIndex == selectedBall)
            {
                buyBtnText.text = "SELECTED";
                buyButton.interactable = false;
            }
            else
            {
                buyBtnText.text = "SELECT";
                buyButton.interactable = true;
            }
        }
        else
        {
            buyBtnText.text = ballPrices[currentIndex].ToString() + " 💎";
            buyButton.interactable = true;
        }
    }
}