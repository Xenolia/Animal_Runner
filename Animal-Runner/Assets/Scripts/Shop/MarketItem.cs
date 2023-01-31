using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MarketItem : MonoBehaviour
{
    [Header("Item Values")]
    [SerializeField] private int itemId;

    [SerializeField] private int price;

    private List<MarketItem> marketItems;

    [SerializeField] private Color[] animalImageColors;

    //[SerializeField] private GameObject Animal;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private Button buyButton, selectButton;

    [SerializeField] private Image animalImage;
    bool hasItem, chosenItem;

    int money, remainingMoney, choosenAnimalID;
    public bool HasItem()
    {
        //0 Satin alinmamis
        //1 Satin alinmis ama kapalý
        //2 Satin alinmis ve sahnede acik
        hasItem = PlayerPrefs.GetInt("item" + itemId.ToString()) != 0;
        return hasItem;
    }

    public bool IsChosen()
    {
        chosenItem = PlayerPrefs.GetInt("item" + itemId.ToString()) == 2;
        MarketController.current.choosenAnimal = this;
        return chosenItem;
    }

    public void InitiliazeItem()
    {
        priceText.text = price.ToString();
        if (HasItem())
        {
            buyButton.gameObject.SetActive(false);
            if (IsChosen())
            {
                ActivateAnimal();
            }
            else
            {
                selectButton.gameObject.SetActive(true);
            }
            animalImage.color = animalImageColors[1];
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            animalImage.color = animalImageColors[0];
        }
    }

    public void BuyAnimal()
    {
        if (!HasItem())
        {
            money = PlayerPrefs.GetInt("Coin");
            if (money >= price)
            {
                remainingMoney = money - price;
                PlayerPrefs.SetInt("Coin", remainingMoney);
                UIManager.current.UpdateCoinText(remainingMoney);
                //PlayerPrefs.SetInt("item" + itemId.ToString(), 1);
                buyButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(true);
                animalImage.color = animalImageColors[1];
                SoundManager.current.PlayBuySound();
                ActivateAnimal();
            }
        }
    }

    public void ActivateAnimal()
    {
        DisableAnimal();

        // MarketController.current.choosenAnimal.itemId = itemId;
        MarketController.current.choosenAnimal = this;
        choosenAnimalID = MarketController.current.choosenAnimal.itemId;
        MarketController.current.animals[choosenAnimalID].SetActive(true);
        selectButton.interactable = false;
        PlayerPrefs.SetInt("item"+itemId.ToString(), 2);
        
    }

    public void DisableAnimal()
    {
        marketItems = MarketController.current.animalIDs;
        /*MarketItem choosenAnimal = MarketController.current.choosenAnimal;

        if (choosenAnimal.gameObject.activeSelf==true)
        {
            //MarketItem marketItem = MarketController.current.animalIDs[choosenAnimal.itemId];
            Debug.Log(choosenAnimal.itemId);
            PlayerPrefs.SetInt("item" + choosenAnimal.itemId, 1);
            MarketController.current.animals[choosenAnimal.itemId].SetActive(false);
            choosenAnimal.selectButton.interactable=true;
        }*/
        foreach (GameObject gmo in MarketController.current.animals)
        {
            gmo.SetActive(false);
        }
        for(int i = 0; i < 6; i++)
        {
            if(PlayerPrefs.GetInt("item"+i) == 2)
            {
                PlayerPrefs.SetInt("item" + i, 1);
            }
            marketItems[i].selectButton.interactable = true;
        }
        MarketController.current.choosenAnimal = null;
    }

}
