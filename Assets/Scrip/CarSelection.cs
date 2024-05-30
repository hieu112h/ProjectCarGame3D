using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public carObject[] cars;
    public GameObject[] carDisplay;
    public List<GameObject> carObjects= new List<GameObject>();
    public Button next;
    public Button preview;
    //public string folder = @"Assets/CarObject";
    public List<carObject> items = new List<carObject>();
    string index;
    public List<int> selected = new List<int>();
    public string[] strings;
    int o =0;

    void Start()
    {
        index = PlayerPrefs.GetString("Car");

        strings = index.Split(',');

        for(int i=0;i<carDisplay.Length;i++)
        {
            carDisplay[i].SetActive(false);
        }
        
        for (int i = 0; i < strings.Length; i++)
        {
            string numberString = strings[i].Substring(1);
            selected.Add(int.Parse(numberString));
            
        }

        for (int i = 0; i < carDisplay.Length; i++)
        {
            for (int j = 0; j < selected.Count; j++)
            {
                if ((i+1) == selected[j])
                {
                    carDisplay[i].SetActive(true);
                    carObjects.Add(carDisplay[i]);
                }
            }
        }
        for(int i=0;i<carObjects.Count;i++)
        {
            carObjects[i].SetActive(false);
        }
        carObjects[0].SetActive(true);

        
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
    public void Next()
    {
        for (int i = 0; i < carObjects.Count; i++)
        {
            carObjects[i].SetActive(false);
        }
        o++;
        carObjects[o].SetActive(true);
        if (o >= carObjects.Count-1)
        {
            o=-1;
        }
    }

    public void Race()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.SetString("Car",index);
        PlayerPrefs.SetInt("Index",o);
    }

}
