using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public carObject[] shopItem;
    public GameObject[] shopPanel;
    public List<carObject> shopList;
    public TMP_Text gold;
    public ShopCar[] shopTemplates;
    public string[] strings;
    public List<carObject> shopItems=new List<carObject>();
    public string abc;
    public int tien;
    void Start()
    {
 
        tien = PlayerPrefs.GetInt("gold");
        gold.text=tien.ToString();
        for (int i = 0; i < shopItem.Length; i++)
        {
            shopPanel[i].SetActive(true);
        }
        LoadPanel();
        CheckBuy();
        get();
    }
    void Update()
    {
        gold.text = tien.ToString();
    }
    public void AddCoins()
    {

    }
    public void CheckBuy()
    {
        for (int i = 0; i < shopItem.Length; i++)
        {
            if (tien >= shopItem[i].price)
            {
                shopTemplates[i].buy.interactable = true;

            }
            else
            {
                shopTemplates[i].buy.interactable = false;
            }
        }

    }
    public void LoadPanel()
    {
        for(int i = 0; i < shopItem.Length; i++)
        {
            shopTemplates[i].nameCar.text = shopItem[i].Name;
            shopTemplates[i].mass.text = shopItem[i].mass.ToString();
            shopTemplates[i].maxSpeed.text=shopItem[i].maxSpeed.ToString();
            shopTemplates[i].price.text= shopItem[i].price.ToString();
            shopTemplates[i].sprite.sprite= shopItem[i].image;
            shopTemplates[i].id.text = shopItem[i].ID;

        }
    }
    //public void back()
    //{
    //    get();
    //}
    public void get()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnData, OnError);
    }
    private void OnData(GetUserDataResult result)
    {
        if (result.Data.TryGetValue("CarId", out var ListJson))
        {
            abc = ListJson.Value;
            PlayerPrefs.SetString("Card", abc);
        }
        Debug.Log(abc);
        strings = abc.Split(',');
        for (int i = 0; i < shopItem.Length; i++)
        {
            for (int j = 0; j < strings.Length; j++)
            {
                if (shopItem[i].ID.Equals(strings[j]))
                {
                    shopPanel[i].SetActive(false);
                }
            }
            
        }
    }
    public void get2()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnData1, OnError);
    }

    private void OnData1(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("Gold"))
        {

            tien = int.Parse(result.Data["Gold"].Value);
            gold.text = tien.ToString();
        }
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
