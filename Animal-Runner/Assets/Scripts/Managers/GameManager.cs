using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Deniz
{
    public class GameManager : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private TestCharacController characController;
        [SerializeField] private SpawnManager spawnManager;

        [Header("Finish")]
        [SerializeField] private GameObject finishLine;

        [Header("Camera")]
        [SerializeField] private GameObject[] cameras;

        float finishDistance, speed, cameraSwapSpeed,gateOpenSpeed;
        GameObject player;
        int distance, coinNumber, currentLevel, previousLevel;

        bool gameBeingPlayed = false;
        [Header("Gate Animation")]
        [SerializeField] private GameObject[] gates;
        [SerializeField] private GameObject Zoo_gate;

        void Start()
        {
            currentLevel = PlayerPrefs.GetInt("Level");
            previousLevel = PlayerPrefs.GetInt("PreviousLevel");
            speed = PlayerPrefs.GetFloat("Speed");
            gateOpenSpeed = PlayerPrefs.GetFloat("GateSpeed");
            MarketController.current.InitiliazeMarketController();
            cameraSwapSpeed = PlayerPrefs.GetFloat("CMSpeed");
            CameraManager.current.SetCameraChangeSpeed(cameraSwapSpeed);

            if (currentLevel % 5 == 0 && currentLevel != previousLevel)
            {
                cameraSwapSpeed -= 0.1f;
                gateOpenSpeed += .25f;
                speed += 0.45f;
                if (cameraSwapSpeed < 0.7f)
                {
                    cameraSwapSpeed = 0.7f;
                    CameraManager.current.SetCameraChangeSpeed(cameraSwapSpeed);
                    PlayerPrefs.SetFloat("CMSpeed", cameraSwapSpeed);
                }
                else
                {
                    CameraManager.current.SetCameraChangeSpeed(cameraSwapSpeed);
                    PlayerPrefs.SetFloat("CMSpeed", cameraSwapSpeed);
                }

                if (speed > 7.8f)
                {
                    speed = 6.9f;
                    PlayerPrefs.SetFloat("Speed", speed);
                    PlayerPrefs.SetInt("PreviousLevel", currentLevel);
                }
                else
                {
                    PlayerPrefs.SetFloat("Speed", speed);
                    PlayerPrefs.SetInt("PreviousLevel", currentLevel);
                }

                if(gateOpenSpeed > 6f)
                {
                    gateOpenSpeed = 6f;
                    PlayerPrefs.SetFloat("GateSpeed", gateOpenSpeed);
                    PlayerPrefs.SetInt("PreviousLevel", currentLevel);
                }
                else
                {
                    PlayerPrefs.SetFloat("GateSpeed", gateOpenSpeed);
                    PlayerPrefs.SetInt("PreviousLevel", currentLevel);
                }

            }

            Debug.Log("Kamera hizi: " + cameraSwapSpeed);
            coinNumber = PlayerPrefs.GetInt("Coin");
            UIManager.current.UpdateCoinText(coinNumber);

            player = characController.gameObject;
            distance = Mathf.RoundToInt(player.transform.position.z);
            UIManager.current.UpdateDistanceText(distance);

            finishDistance = (25 + currentLevel * 15) ;
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

        #region GameStates
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
            coinNumber = PlayerPrefs.GetInt("Coin");
            spawnManager.StopObjectPool();
            UIManager.current.OpenLosePanel(coinNumber, distance);
            SoundManager.current.PlayLoseGameSound();

        }


        public void WinTheGame()
        {
            coinNumber = PlayerPrefs.GetInt("Coin");
            PlayerPrefs.SetInt("Coin", coinNumber + 50);
            gameBeingPlayed = false;
            spawnManager.StopObjectPool();
            UIManager.current.OpenWinPanel(coinNumber, distance);
            SoundManager.current.PlayWinGameSound();
        }

        public void ContiuneTheGame()
        {
            spawnManager.CloseObstacles();
            characController.RestartTheMovement();
            gameBeingPlayed = true;
            spawnManager.StartObjectPool();
            if (PlayerPrefs.GetInt("Music") == 1)
            {
                SoundManager.current.PlayGameMusic();
            }
        }
        #endregion

        private void ShowFinishLine()
        {
            if (distance >= finishDistance - 20)
            {
                finishLine.SetActive(true);
            }
        }

        public void AddCoin()
        {
            coinNumber = PlayerPrefs.GetInt("Coin") + 1;
            PlayerPrefs.SetInt("Coin", coinNumber);
            UIManager.current.UpdateCoinText(coinNumber);
        }

        private void OpenTheGates()
        {
            gates[0].transform.DORotate(new Vector3(0, -90, 0), gateOpenSpeed, RotateMode.LocalAxisAdd);
            gates[1].transform.DORotate(new Vector3(0, 90, 0), gateOpenSpeed, RotateMode.LocalAxisAdd);
        }

        #region Revive
        public void RevivePlayer()
        {
            characController.Revive();
        }
        #endregion
    }
}

