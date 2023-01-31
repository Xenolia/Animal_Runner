using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private TestCharacController characController;
    [SerializeField] private SpawnManager spawnManager;

    [Header("Finish")]
    [SerializeField] private GameObject finishLine;

    [Header("Camera")]
    [SerializeField] private GameObject[] cameras;

    float finishDistance, speed, cameraSwapSpeed;
    GameObject player;
    int distance,coinNumber,currentLevel,previousLevel;
    
    bool gameBeingPlayed = false;
    [SerializeField] private GameObject[] gates;

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        previousLevel = PlayerPrefs.GetInt("PreviousLevel");
        speed = PlayerPrefs.GetFloat("Speed");
        MarketController.current.InitiliazeMarketController();
        cameraSwapSpeed = PlayerPrefs.GetFloat("CMSpeed");
        CameraManager.current.SetCameraChangeSpeed(cameraSwapSpeed);

        if (currentLevel %5 == 0 && currentLevel !=previousLevel)
        {
            cameraSwapSpeed -= 0.1f;
            speed += 0.2f;
            PlayerPrefs.SetFloat("Speed", speed);
            PlayerPrefs.SetInt("PreviousLevel", currentLevel);
            CameraManager.current.SetCameraChangeSpeed(cameraSwapSpeed);
            PlayerPrefs.SetFloat("CMSpeed", cameraSwapSpeed);
        }

        Debug.Log("Kamera hizi: " + cameraSwapSpeed);
        coinNumber = PlayerPrefs.GetInt("Coin");
        UIManager.current.UpdateCoinText(coinNumber);

        player = characController.gameObject;
        distance = Mathf.RoundToInt(player.transform.position.z);
        UIManager.current.UpdateDistanceText(distance);

        finishDistance = currentLevel * 30+1;
        finishLine.transform.position = new Vector3(transform.position.x, transform.position.y, finishDistance);
    }

    private void Update()
    {
        if (gameBeingPlayed)
        {
            distance = Mathf.RoundToInt(player.transform.position.z);
            UIManager.current.UpdateDistanceText(distance);
            ShowFinishLine();
        }    
    }
    public void StartTheGame()
    {
        gameBeingPlayed = true;
        UIManager.current.CloseStartPanelObjects();
        characController.StartMovement(speed);
        spawnManager.StartObjectPool();
        CameraManager.current.ChangeCamera(CameraManager.current.cameras[1]);
        OpenTheGates();
    }

    public void LoseTheGame()
    {
        gameBeingPlayed = false;
        spawnManager.StopObjectPool();
        UIManager.current.OpenLosePanel(coinNumber, distance);
    }

    
    public void WinTheGame()
    {
        coinNumber = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", coinNumber + 50);
        gameBeingPlayed = false;
        spawnManager.StopObjectPool();
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
        UIManager.current.UpdateCoinText(coinNumber);
    }

    private void OpenTheGates()
    {
        gates[0].transform.DORotate(new Vector3(0, -90, 0), 4f, RotateMode.LocalAxisAdd);
        gates[1].transform.DORotate(new Vector3(0, 90, 0), 4f, RotateMode.LocalAxisAdd);
    }
}
