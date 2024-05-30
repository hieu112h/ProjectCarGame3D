using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarManage : MonoBehaviour
{
    public carObject[] cars;
    public GameObject[] carDisplay;
    public List<GameObject> carObjects = new List<GameObject>();
    public List<carObject> items = new List<carObject>();
    string index;
    public List<int> selected = new List<int>();
    public string[] strings;
    public int o;

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetString("Car");
        o = PlayerPrefs.GetInt("Index");
        Debug.Log(index);
        Debug.Log(o);
        strings = index.Split(',');
        //.Log(strings.Length);
        for (int i = 0; i < carDisplay.Length; i++)
        {
            carDisplay[i].SetActive(false);
        }

        for (int i = 0; i < strings.Length; i++)
        {
            String numberString = strings[i].Substring(1);
            selected.Add(int.Parse(numberString));
            //Debug.Log(selected[i]);
        }
        //for(int i=0;i<selected.Count;i++)
        //{
        //    carDisplay[selected[i]-1].SetActive(true);
        //}
        for (int i = 0; i < carDisplay.Length; i++)
        {
            for (int j = 0; j < selected.Count; j++)
            {
                if ((i + 1) == selected[j])
                {
                    //carDisplay[i].SetActive(true);
                    carObjects.Add(carDisplay[i]);
                }
            }
        }
        carObjects[o].SetActive(true);

        //for (int i=0; i<cars.Length; i++)
        //{
        //    for(int j=0; j<strings.Length; j++)
        //    {
        //        if (cars[i].ID == strings[j])
        //        {
        //            Debug.Log(i);
        //        }
        //    }
        //}
        //foreach (var item in cars)
        //{
        //    foreach(var car in strings)
        //    {
        //        if (item.ID.Equals(car))
        //        {
        //            Debug.Log(item.ID);
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (index > 2)
        //{
        //    next.interactable=false;
        //}
        //else
        //{
        //    next.interactable=true;
        //}
        //if (index<=0)
        //{
        //    preview.interactable=false;
        //}
        //else
        //{
        //    preview.interactable=true;
        //}
    }

    
    
    //public void Preview()
    //{
    //    index--;
    //    for(int i=0;i<cars.Length;i++)
    //    {
    //        cars[i].SetActive(false) ;
    //        cars[index].SetActive(true) ;

    //    }

    //    PlayerPrefs.SetInt("carIndex",index);
    //    PlayerPrefs.Save();
    //}
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}