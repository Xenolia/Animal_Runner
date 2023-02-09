using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AdManager _adManager;
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

    public TextMeshProUGUI TimeText;

    int Coins ;
    int levelIndex;
    int currentSceneIndex;

    private Coroutine currentCoroutine;

    private Action OnTestButtonClickedEvent;

    #region Event Example
    private void OnTestButtonClickedMethod1()
    {
        Debug.Log("Test Button clicked1");
    }

    private void OnTestButtonClickedMethod2()
    {
        Debug.Log("Test Button clicked2");
    }

    [NaughtyAttributes.Button("Test Button")]
    private void ClickTestButton()
    {
        OnTestButtonClickedEvent?.Invoke();
    }


    [NaughtyAttributes.Button("Register Event")]
    private void RegisterTestEvent()
    {
        OnTestButtonClickedEvent += OnTestButtonClickedMethod1;
        OnTestButtonClickedEvent += OnTestButtonClickedMethod2;
    }

    #endregion
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

    #region Panels
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

    private void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void CloseStartPanelObjects()
    {
        CloseLevelText();
        CloseShop();
        CloseSettingsButton();
    }
    #endregion

    #region Texts
    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance.ToString()+"m";
    }
    #endregion

    #region Buttons
    public void PlayAgain()
    {
        int previousLevelIndex = levelIndex;
        PlayerPrefs.SetInt("PreviousLevel", previousLevelIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextLevel()
    {
        Debug.Log("asda");
        if(_adManager.InterstatialAdManager.IsInterstatialAdReady())
        {
            _adManager.InterstatialAdManager.RegisterOnAdClosedEvent(OnInterstatialAdClosed);
            _adManager.InterstatialAdManager.ShowAd();
        }
        else
        {
            levelIndex++;
            PlayerPrefs.SetInt("Level", levelIndex);
            SceneManager.LoadScene(currentSceneIndex);
        }

    }

    private void OnInterstatialAdClosed(IronSourceAdInfo info)
    {
        _adManager.InterstatialAdManager.UnRegisterOnAdClosedEvent(OnInterstatialAdClosed);

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
        if(_adManager.RewardedAdManager.IsRewardedAdReady())
        {
            _adManager.RewardedAdManager.RegisterOnUserEarnedRewarededEvent(OnUserEarnedReward);
            _adManager.RewardedAdManager.ShowAd();
        }
    }

    private void OnUserEarnedReward(IronSourcePlacement placement, IronSourceAdInfo info)
    {
        Coins = PlayerPrefs.GetInt("Coin");
        Coins *= 2;
        PlayerPrefs.SetInt("Coin", Coins);
        WPGatheredCoinText.text = Coins.ToString();
        buttons[1].SetActive(false);
    }
    private void CloseSettingsButton()
    {
        buttons[2].gameObject.SetActive(false);
    }

    private void OpenSettingsButton()
    {
        buttons[2].gameObject.SetActive(true);
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
    private void CloseLevelText()
    {
        levelTextParent.SetActive(false);
    }
    #endregion    

    public void ContiuneButton()
    {
        gameManager.RevivePlayer();
        CloseLosePanel();
        buttons[7].gameObject.SetActive(false);
        currentCoroutine = StartCoroutine(TimeTextAnimation());
        StartCoroutine(StopAfter());
    }

    IEnumerator TimeTextAnimation()
    {
        TimeText.gameObject.SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(true);
        int time=3;
        while (true)
        {
            TimeText.text = time.ToString();
            time -= 1;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StopAfter()
    {
        yield return new WaitForSeconds(3f);
        TimeText.gameObject.SetActive(false);
        StopCoroutine(currentCoroutine);
        gameManager.ContiuneTheGame();
    }
}
