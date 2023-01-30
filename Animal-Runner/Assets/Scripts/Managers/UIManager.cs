using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager current;

    [Header("Panels")]
    [SerializeField] private GameObject[] panels;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI coinText,distanceText;
    [SerializeField] private TextMeshProUGUI WPGatheredCoinText, LPGatheredCoinText;
    [SerializeField] private TextMeshProUGUI WPTravelledDText, LPTravelledDText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Game Start")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject levelTextParent;

    [Header("Buttons")]
    [SerializeField] private GameObject[] buttons;

    [Header("Shop")]
    [SerializeField] private GameObject shopPanel;

    [Header("Options")]
    [SerializeField] private GameObject optionsPanel;

    int Coins ;
    int levelIndex;
    int currentSceneIndex;
    private void Start()
    {
        current = this;
        levelIndex = PlayerPrefs.GetInt("Level");
        levelText.text = levelIndex.ToString();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void OpenWinPanel(int gatheredCoins,int tDistance)
    {
        panels[0].SetActive(true);
        panels[2].SetActive(false);
        WPGatheredCoinText.text = gatheredCoins.ToString() + " + " + 50;
        WPTravelledDText.text = tDistance.ToString();
    }

    public void CloseWinPanel()
    {
        panels[0].SetActive(false);
        panels[2].SetActive(false);
    }

    public void OpenLosePanel(int gatheredCoins,int tDistance)
    {
        panels[1].SetActive(true);
        panels[2].SetActive(false);
        LPGatheredCoinText.text = gatheredCoins.ToString();
        LPTravelledDText.text = tDistance.ToString();
    }

    public void CloseLosePanel()
    {
        panels[1].SetActive(false);
        panels[2].SetActive(false);
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance.ToString()+"m";
    }

    public void PlayAgain()
    {
        int previousLevelIndex = levelIndex;
        PlayerPrefs.SetInt("PreviousLevel", previousLevelIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void StartButton()
    {
        gameManager.StartTheGame();
        buttons[0].SetActive(false);
    }

    public void DoubleCoinButton()
    {
        Coins = PlayerPrefs.GetInt("Coin");
        Coins *= 2;
        PlayerPrefs.SetInt("Coin", Coins);
        WPGatheredCoinText.text = Coins.ToString();
        buttons[1].SetActive(false);
    }

    private void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    private void CloseLevelText()
    {
        levelTextParent.SetActive(false);
    }

    public void OpenOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

    private void CloseSettingsButton()
    {
        buttons[2].gameObject.SetActive(false);
    }
    public void CloseStartPanelObjects()
    {
        CloseLevelText();
        CloseShop();
        CloseSettingsButton();
    }
}
