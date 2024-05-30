using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Car", menuName = "Items/Car")]

public class carObject : ScriptableObject
{
    public string ID;
    public string Name;
    public int mass;
    public GameObject car;
    public Sprite image;
    public int maxSpeed;
    public int price;
}
