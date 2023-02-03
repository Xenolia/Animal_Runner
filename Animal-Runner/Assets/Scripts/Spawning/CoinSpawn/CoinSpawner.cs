using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public List<GameObject> coins;
    private float coinXPos;
    [SerializeField] private Transform playerTransform;

    void SpawnCoin()
    {
        foreach (GameObject coin in coins)
        {
            if(coin.activeSelf == false)
            {
                coin.SetActive(true);

                coinXPos = Random.Range(-0.65f, 0.65f);

                //coin.transform.position = new Vector3(coinXPos, 0.08f, playerTransform.position.z+10f);
                coin.transform.localPosition = new Vector3(coinXPos, 0.08f, playerTransform.position.z + 10f);
                return;
            }
        }
    }

    public void CreateCoins()
    {
        InvokeRepeating("SpawnCoin", 1f, 1f);
    }

    public void StopCreatingCoins()
    {
        CancelInvoke();
    }
}
