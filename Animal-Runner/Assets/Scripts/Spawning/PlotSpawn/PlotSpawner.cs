using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    //dikey ýnputda karakter zýplayacak
    private int initAmaount = 8;
    private float plotSize = 5.5f;
    private float xPosLeft = -4.065f;
    private float xPosRight = 4.065f;
    private float lastZPos = -9.88f;

    GameObject plotLeft;
    GameObject plotRight;
    public List<GameObject> plots;

    [SerializeField]private RoadSpawner roadSpawner;
    void Start()
    {
        SpawnPlot();
    }

    public void SpawnPlot()
    {
        for (int i = 0; i < initAmaount; i++)
        {
            plotLeft = plots[Random.Range(0, plots.Count)];
            plotRight = plots[Random.Range(0, plots.Count)];

            float zPos = lastZPos + plotSize;

            Instantiate(plotLeft, new Vector3(xPosLeft, 0, zPos), plotLeft.transform.rotation,roadSpawner.roads[i].transform);
            Instantiate(plotRight, new Vector3(xPosRight, 0, zPos), new Quaternion(0, 180, 0, 0), roadSpawner.roads[i].transform);

            lastZPos += plotSize;
            
        }
        
    }
}
