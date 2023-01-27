using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> cars, vans, trucks;

    [SerializeField]private Transform playerTransform;
    float carXPos, vanXPos, truckXPos;
    int carIndex, vanIndex, truckIndex;
    void Awake()
    {
        SpawnCars();
        SpawnVans();
        SpawnTrucks();
    }

    private void SpawnCars()
    {
        carIndex = Random.Range(0, cars.Count);
        if(cars[carIndex].activeSelf == false)
        {
            cars[carIndex].SetActive(true);
            carXPos = Random.Range(-0.465f, 0.465f);
            cars[carIndex].transform.position = new Vector3(carXPos, -0.02f, playerTransform.position.z + 10f);
        }
        else
        {
            foreach (GameObject car in cars)
            {
                if(car.activeSelf == false)
                {
                    car.SetActive(true);
                    carXPos = Random.Range(-0.465f, 0.465f);
                    car.transform.position = new Vector3(carXPos, -0.02f, playerTransform.position.z + 10f);
                    return;
                }
            }
        }
    }

    private void SpawnVans()
    {
        vanIndex = Random.Range(0, vans.Count);
        if (vans[vanIndex].activeSelf == false)
        {
            vans[vanIndex].SetActive(true);
            vanXPos = Random.Range(-0.4f, 0.4f);
            vans[vanIndex].transform.position = new Vector3(vanXPos, 0, playerTransform.position.z + 12.5f);
        }
        else
        {
            foreach (GameObject van in vans)
            {
                if (van.activeSelf == false)
                {
                    van.SetActive(true);
                    vanXPos = Random.Range(-0.4f, 0.4f);
                    van.transform.position = new Vector3(vanXPos, 0, playerTransform.position.z + 12.5f);
                    return;
                }
            }
        }
    }

    private void SpawnTrucks()
    {
        truckIndex = Random.Range(0, trucks.Count);
        if (trucks[truckIndex].activeSelf == false)
        {
            trucks[truckIndex].SetActive(true);
            truckXPos = Random.Range(-0.35f, 0.35f);
            trucks[truckIndex].transform.position = new Vector3(truckXPos, -0.015f, playerTransform.position.z + 16f);
        }
        else
        {
            foreach (GameObject truck in trucks)
            {
                if (truck.activeSelf == false)
                {
                    truck.SetActive(true);
                    truckXPos = Random.Range(-0.35f, 0.35f);
                    truck.transform.position = new Vector3(truckXPos, -0.015f, playerTransform.position.z + 16f);
                    return;
                }
            }
        }
    }

    public void CreateVehicles()
    {
        InvokeRepeating("SpawnCars", 3f, 3f);
        InvokeRepeating("SpawnVans", 3f, 3f);
        InvokeRepeating("SpawnTrucks", 3f, 6f);
    }

    public void StopCreatingVehicles()
    {
        CancelInvoke();
    }
}
