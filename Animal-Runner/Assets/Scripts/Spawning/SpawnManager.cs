using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;

    public ObstacleSpawner obstacleSpawner;

    public CoinSpawner coinSpawner;
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
    }

    public void SpawnPointTriggered()
    {
        roadSpawner.MoveRoad();
    }

    public void StartObjectPool()
    {
        obstacleSpawner.CreateVehicles();
        coinSpawner.CreateCoins();
    }

    public void StopObjectPool()
    {
        obstacleSpawner.StopCreatingVehicles();
        coinSpawner.StopCreatingCoins();
    }

    public void CloseObstacles()
    {
        obstacleSpawner.CloseAllActiveVehicles();
    }
}
