using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    public static MarketController current;
    public List<MarketItem> animalIDs;
    public List<GameObject> animals;
    public MarketItem choosenAnimal;

    private int activeIndex;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }
    public void InitiliazeMarketController()
    {

        if(!PlayerPrefs.HasKey("item0"))
        {
            DefaultMarketController();
        }
        else
        {
            foreach (MarketItem animal in animalIDs)
            {
                animal.InitiliazeItem();
            }
        }     
    }

    public void DefaultMarketController()
    {
        choosenAnimal = animalIDs[0];
        choosenAnimal.ActivateAnimal();
        foreach (MarketItem animal in animalIDs)
        {
            animal.InitiliazeItem();
        }
        //current = this;
    }

    public int ReturnActiveAnimalIndex()
    {
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i].activeSelf)
            {
                activeIndex = i;
                break;
            }
        }
        return activeIndex;
    }
}
