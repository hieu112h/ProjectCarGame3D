using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Display : MonoBehaviour
{
    public string abs;

    public List<carObject> items = new List<carObject>();
    public TMP_Text userName;
    public TMP_Text gold;
    public string[] strings;
    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> games= new List<GameObject>();
    public List<GameObject> clone;
    public Vector3 scaleMultiplier = new Vector3(2f, 2f, 2f);
    public GameObject currentGameObject;
    string stringListJson;
    void Start()
    {
        userName.text = PlayerPrefs.GetString("UserName");
        //LoadCar();
        int childCount = transform.childCount;

        
        for (int i = 0; i < childCount; i++)
        {
           games.Add(transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < games.Count; i++)
        {
            games[i].SetActive(true);
            for(int y = i; y < games.Count; y++)
            {
                games[y].SetActive(false);  
            }
        }
        for (int i = 0; i < games.Count; i++)
        {
            if (games[i] == objects[i])
            {
                games[i].SetActive(true);
            }
            else
            {
                games[i].SetActive(false);
            }
        }

        get();
    }



    public void get()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnData, OnError);
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        throw new NotImplementedException();
    }

    private void OnData(GetUserDataResult result)
    {

        if (result.Data.TryGetValue("CarId", out var ListJson))
        {
            stringListJson = ListJson.Value;


            abs = stringListJson;
            Tach();

        }
        if (result.Data.ContainsKey("Gold"))
        {
            
            int goldAmount = int.Parse(result.Data["Gold"].Value);
            gold.text= goldAmount.ToString();
            PlayerPrefs.SetInt("gold",goldAmount);
        }
        Hthi();
        PlayerPrefs.SetString("Car", abs);

    }
    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    public void Tach()
    {
        strings = abs.Split(',');

    }
    //public void LoadCar()
    //{
    //    if (Directory.Exists(folder))
    //    {
    //        string[] files = Directory.GetFiles(folder);
    //        foreach (string file in files)
    //        {
    //            carObject car = AssetDatabase.LoadAssetAtPath<carObject>(file);
    //            if (car != null)
    //            {
    //                items.Add(car);
    //            }
    //        }
    //    }

    //}
    public void Hthi()
    {
        foreach (var i1 in items)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                if (i1.ID.Equals(strings[i]))
                {

                    objects.Add(i1.car);

                }
            }
        }
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            clone.Add(Instantiate(objects[i], transform.position, Quaternion.identity));
            clone[i].transform.parent = transform;
            clone[i].transform.localScale = new Vector3(
                    clone[i].transform.localScale.x * scaleMultiplier.x,
                    clone[i].transform.localScale.y * scaleMultiplier.y,
                    clone[i].transform.localScale.z * scaleMultiplier.z
                );
            yield return new WaitForSeconds(2);
            for(int y=i;y>=0;y--)
            {
                clone[y].SetActive(false);
            }
        }
        for(int u = 0; u <= 20; u++)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                clone[i].SetActive(true);
                yield return new WaitForSeconds(2);
                for (int y = i; y >= 0; y--)
                {
                    clone[y].SetActive(false);
                }
            }
        }
    }
    public void shop()
    {
        PlayerPrefs.SetString("Car", abs);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void select()
    {
        PlayerPrefs.SetString("Car",abs);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void setting()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

}
