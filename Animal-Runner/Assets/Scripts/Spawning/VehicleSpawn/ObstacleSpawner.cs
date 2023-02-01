using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> cars, vans, trucks, barriers;

    [SerializeField]private Transform playerTransform;
    float carXPos, vanXPos, truckXPos;
    int carIndex, vanIndex, truckIndex,barrierIndex;
    void Awake()
    {
        //SpawnCars();
        //SpawnVans();
        //SpawnTrucks();
    }

    private void SpawnCars()
    {
        carIndex = Random.Range(0, cars.Count);
        if(cars[carIndex].activeSelf == false)
        {
            cars[carIndex].SetActive(true);
            carXPos = Random.Range(-0.55f, 0.55f);
            cars[carIndex].transform.position = new Vector3(carXPos, -0.02f, playerTransform.position.z + 10f);
        }
        else
        {
            foreach (GameObject car in cars)
            {
                if(car.activeSelf == false)
                {
                    car.SetActive(true);
                    carXPos = Random.Range(-0.55f, 0.55f);
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
            vanXPos = Random.Range(-0.45f, 0.45f);
            vans[vanIndex].transform.position = new Vector3(vanXPos, 0, playerTransform.position.z + 12.5f);
        }
        else
        {
            foreach (GameObject van in vans)
            {
                if (van.activeSelf == false)
                {
                    van.SetActive(true);
                    vanXPos = Random.Range(-0.45f, 0.45f);
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

    private void SpawnBarriers()
    {
        barrierIndex = Random.Range(0, barriers.Count);
        if (barriers[barrierIndex].activeSelf == false)
        {
            barriers[barrierIndex].SetActive(true);
           // barrierXPos = Random.Range(-0.35f, 0.35f);
            barriers[barrierIndex].transform.localPosition = new Vector3(0, 0f, playerTransform.position.z + 18.2f);
        }
        else
        {
            foreach (GameObject barrier in barriers)
            {
                if (barrier.activeSelf == false)
                {
                    barrier.SetActive(true);
                    //barrierXPos = Random.Range(-0.35f, 0.35f);
                    barrier.transform.localPosition = new Vector3(0, 0f, playerTransform.position.z + 18.2f);
                    return;
                }
            }
        }
    }

    public void CreateVehicles()
    {
        InvokeRepeating("SpawnCars", 0f, 3f);
        InvokeRepeating("SpawnVans", 0f, 3f);
        InvokeRepeating("SpawnTrucks", 0f, 6f);
        InvokeRepeating("SpawnBarriers", 0f, 6f);
    }

    public void StopCreatingVehicles()
    {
        CancelInvoke();
    }
}
