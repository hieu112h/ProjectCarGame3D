using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : MonoBehaviour
{
    public string PlayerName;
    public float TimeFinish;
    public PlayerRecord(string play,float time)
    {
        PlayerName = play;
        TimeFinish = time;
    }
}
