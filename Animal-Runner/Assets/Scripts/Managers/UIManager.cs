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

        if(PlayerPrefs.GetInt("Music") == 1)
        {
            buttons[4].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(true);
        }
        else
        {
            buttons[4].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(false);
        }

        if(PlayerPrefs.GetInt("Sounds") == 1)
        {
            buttons[5].gameObject.SetActive(true);
            buttons[6].gameObject.SetActive(false);
        }
        else
        {
            buttons[5].gameObject.SetActive(false);
            buttons[6].gameObject.SetActive(true);
        }
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
        CloseSettingsButton();
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
        OpenSettingsButton();
    }

    private void CloseSettingsButton()
    {
        buttons[2].gameObject.SetActive(false);
    }

    private void OpenSettingsButton()
    {
        buttons[2].gameObject.SetActive(true);
    }
    public void CloseStartPanelObjects()
    {
        CloseLevelText();
        CloseShop();
        CloseSettingsButton();
    }

    public void CloseMusicButton()
    {
        buttons[4].gameObject.SetActive(true);
        buttons[3].gameObject.SetActive(false);
        SoundManager.current.CloseGameMusic();
        PlayerPrefs.SetInt("Music", 0);
    }

    public void OpenMusicButton()
    {
        buttons[4].gameObject.SetActive(false);
        buttons[3].gameObject.SetActive(true);
        SoundManager.current.PlayGameMusic();
        PlayerPrefs.SetInt("Music", 1);
    }

    public void CloseSoundButton()
    {
        buttons[5].gameObject.SetActive(false);
        buttons[6].gameObject.SetActive(true);
        SoundManager.current.MuteSounds();
        PlayerPrefs.SetInt("Sounds", 0);
    }
    public void OpenSoundButton()
    {
        buttons[5].gameObject.SetActive(true);
        buttons[6].gameObject.SetActive(false);
        SoundManager.current.ActivateSounds();
        PlayerPrefs.SetInt("Sounds", 1);
    }
}
