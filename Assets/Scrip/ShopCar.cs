using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopCar : MonoBehaviour
{
    public ShopManager shopManager;
    public GameObject abc;
    public TMP_Text id;
    public TMP_Text nameCar;
    public TMP_Text mass;
    public Image sprite;
    public TMP_Text maxSpeed;
    public TMP_Text price;
    public Button buy;
    public List<string> carId;
    string stringListJson;
    public void GetName()
    {
        int price1=int .Parse(price.text);
        if (shopManager.tien >= price1)
        {
            shopManager.tien -= price1;
            shopManager.CheckBuy();
            get();

        }
    }
    public void save(string idcar,int gold)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"CarId",idcar },
                {"Gold",gold.ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    public void get()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnData, OnError);
    }

    private void OnData(GetUserDataResult result)
    {
        if (result.Data.TryGetValue("CarId", out var ListJson))
        {
            stringListJson = ListJson.Value;
            stringListJson = stringListJson + "," + id.text;
            save(stringListJson,shopManager.tien);
            abc.SetActive(false);
        }
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Luu thanh cong");
    }
    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    public void Back()
    {
        PlayerPrefs.SetString("carId",stringListJson);
        Debug.Log(stringListJson);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
