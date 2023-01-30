using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private TestCharacController characController;
    [SerializeField] private SpawnManager spawnManager;
    //[SerializeField] private UIManager uIManager;
    [Header("Finish")]
    [SerializeField] private GameObject finishLine;

    [Header("Camera")]
    [SerializeField] private GameObject[] cameras;
    //[SerializeField] float finishDistance;
    float finishDistance, speed, cameraSwapSpeed;
    GameObject player;
    int distance,coinNumber,currentLevel,previousLevel;
    
    bool gameBeingPlayed = false;

    private CinemachineBrain cinemachineBrain;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        previousLevel = PlayerPrefs.GetInt("PreviousLevel");
        speed = PlayerPrefs.GetFloat("Speed");
        MarketController.current.InitiliazeMarketController();
        SetCameraChangeSpeed();

        if (currentLevel %5 == 0 && currentLevel !=previousLevel)
        {
            cameraSwapSpeed -= 0.1f;
            speed += 0.2f;
            PlayerPrefs.SetFloat("Speed", speed);
            PlayerPrefs.SetInt("PreviousLevel", currentLevel);
            cinemachineBrain.m_DefaultBlend.m_Time = cameraSwapSpeed;
            PlayerPrefs.SetFloat("CMSpeed", cameraSwapSpeed);
        }

        Debug.Log("Kamera hizi: " + cameraSwapSpeed);
        coinNumber = PlayerPrefs.GetInt("Coin");
        //uIManager.UpdateCoinText(coinNumber);
        UIManager.current.UpdateCoinText(coinNumber);
        player = characController.gameObject;
        distance = Mathf.RoundToInt(player.transform.position.z);
        //uIManager.UpdateDistanceText(distance);
        UIManager.current.UpdateDistanceText(distance);
        finishDistance = currentLevel * 30+1;
        finishLine.transform.position = new Vector3(transform.position.x, transform.position.y, finishDistance);
    }

    private void Update()
    {
        if (gameBeingPlayed)
        {
            distance = Mathf.RoundToInt(player.transform.position.z);
            //uIManager.UpdateDistanceText(distance);
            UIManager.current.UpdateDistanceText(distance);
            ShowFinishLine();
        }    
    }
    public void StartTheGame()
    {
        gameBeingPlayed = true;
        // uIManager.CloseShop();
        //UIManager.current.CloseShop();
        //UIManager.current.CloseLevelText();
        UIManager.current.CloseStartPanelObjects();
        characController.StartMovement(speed);
        spawnManager.StartObjectPool();
        cameras[0].SetActive(false);
        cameras[1].SetActive(true);
    }

    public void LoseTheGame()
    {
        gameBeingPlayed = false;
        spawnManager.StopObjectPool();
        //uIManager.OpenLosePanel(coinNumber,distance);
        UIManager.current.OpenLosePanel(coinNumber, distance);
    }

    
    public void WinTheGame()
    {
        coinNumber = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", coinNumber + 50);
        gameBeingPlayed = false;
        spawnManager.StopObjectPool();
        //uIManager.OpenWinPanel(lastCoinCount, distance);
        UIManager.current.OpenWinPanel(coinNumber, distance);
    }

    private void ShowFinishLine()
    {
        if(distance >= finishDistance-20)
        {
            finishLine.SetActive(true);
        }
    }
    
    public void AddCoin()
    {
        coinNumber=PlayerPrefs.GetInt("Coin")+1;
        PlayerPrefs.SetInt("Coin", coinNumber);
       //uIManager.UpdateCoinText(coinNumber);
        UIManager.current.UpdateCoinText(coinNumber);
    }

    private void SetCameraChangeSpeed()
    {
        cinemachineBrain = Camera.main.gameObject.GetComponent<CinemachineBrain>();
        cameraSwapSpeed = PlayerPrefs.GetFloat("CMSpeed");
        cinemachineBrain.m_DefaultBlend.m_Time = cameraSwapSpeed;
    }
}
