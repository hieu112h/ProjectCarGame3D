using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadBoard : MonoBehaviour
{
    public List<PlayerRecord> PlayerRecords = new List<PlayerRecord>();
    // Start is called before the first frame update
    public void AddPlayer(string play, float time)
    {
        PlayerRecord player=new PlayerRecord(play, time);
        PlayerRecords.Add(player);
        
    }
    public void Update()
    {
        Debug.Log(PlayerRecords.Count);
    }
    public void SortRecord()
    {
        PlayerRecords.Sort((record1, record2) => record1.TimeFinish.CompareTo(record2.TimeFinish));
    }
    public void Display()
    {
        foreach (var record in PlayerRecords)
        {
            Debug.Log(record.PlayerName+":"+record.TimeFinish);
        }
    }
}
