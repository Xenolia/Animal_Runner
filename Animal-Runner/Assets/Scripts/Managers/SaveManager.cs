using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private AdManager _adManager;
    [SerializeField] private InAppPurchase _inAppPurchase;
    private void Awake()
    {
        Debug.Log("Test");
        _adManager.Init();
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("PreviousLevel", 1);
        }
        if (!PlayerPrefs.HasKey("Speed"))
        {
            PlayerPrefs.SetFloat("Speed", 1f);
        }

        if (!PlayerPrefs.HasKey("CMSpeed"))
        {
            PlayerPrefs.SetFloat("CMSpeed", 1f);
        }

        if (!PlayerPrefs.HasKey("Sounds"))
        {
            PlayerPrefs.SetInt("Sounds", 1);
        }
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }
}
