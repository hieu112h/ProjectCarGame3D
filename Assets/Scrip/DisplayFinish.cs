using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class DisplayFinish : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public LeadBoard board;
    public bool display=false;
    void Update()
    {
        if (!display)
        {
            //if(board.PlayerRecords.Count == 1)
            //{
                text.text = board.PlayerRecords[0].PlayerName + " : " + chuyen(board.PlayerRecords[0].TimeFinish);
            if (board.PlayerRecords[0].PlayerName.Equals("Player1"))
            {
                text4.text = "+ 40";
            }

            //}if(board.PlayerRecords.Count == 2)
            //{
            text2.text = board.PlayerRecords[1].PlayerName + " : " + chuyen(board.PlayerRecords[1].TimeFinish);
            if (board.PlayerRecords[1].PlayerName.Equals("Player1"))
            {
                text4.text = "+ 20";
            }
            //}
            //if (board.PlayerRecords.Count == 3)
            //{
            text3.text = board.PlayerRecords[2].PlayerName + " : " + chuyen(board.PlayerRecords[2].TimeFinish);
            if (board.PlayerRecords[2].PlayerName.Equals("Player1"))
            {
                text4.text = "+ 10";
            }
            //}

        }
        }
    string acd;
    public string chuyen(float abc)
    {
        int minutes = Mathf.FloorToInt(abc / 60);
        int seconds = Mathf.FloorToInt(abc % 60);
        int milliseconds = Mathf.FloorToInt((abc * 1000) % 1000);
        acd = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds,milliseconds);
        return acd;
    }
}
