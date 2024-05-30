using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CarPosition : MonoBehaviour
{
    
    
    
    public float time;
    public Timer timer;
    public LeadBoard board;
    public Transform car;
    public List<Transform> tags;
    public GameObject canvas;
    public GameObject canvas2;
    public Transform[] points; // Mảng chứa các tham chiếu đến transform của các điểm Point trên đường
    public int[] tagPositions; // Mảng để lưu trữ vị trí của từng xe trên đường
    public TMP_Text text;
    public bool finish=false;
    int playerPositon;
    public AudioManage audioManage;
    void Start()
    {
        Rigidbody[] rigidbodies = car.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            tags.Add(rigidbody.transform);
        }
        //Transform[] transforms=car.GetCom(0);
       
        Debug.Log(tags.Count);
        tagPositions = new int[tags.Count];
        audioManage=GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManage>();
        



        // Khởi tạo mảng vị trí của các Tag
        //tagPositions = new int[tags.Length];
        //for(int i=0;i<tags.Length; i++)
        //{
        //    if (tags[i].CompareTag("Player1"))
        //    playerPositon = i + 1;
        //}
        //Debug.Log(playerPositon+"/"+tags.Length);
        //s1();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < tags.Count; i++)
        {
            float minDistance = Mathf.Infinity;
            int closestPointIndex = 0;

            
            for (int j = 0; j < points.Length; j++)
            {
                
                float distance = Vector3.Distance(tags[i].position, points[j].position);
                //Debug.Log(distance);

                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPointIndex = j;
                }
                
            }
            tagPositions[i] = closestPointIndex;
            if (tags[i].CompareTag("Player1") && !finish)
            {
                if (tagPositions[i] == points.Length - 1)
                {
                    time=timer.elapsedTime;
                    board.AddPlayer(tags[i].tag,time);
                    canvas.SetActive(false);
                    canvas2.SetActive(true);
                    audioManage.PlaySFX2();
                    finish = true;
                }
            }
                    



        }


        for (int i = 0; i < tagPositions.Length - 1; i++)
        {
            for (int j = i + 1; j < tagPositions.Length; j++)
            {
                if (tagPositions[i] < tagPositions[j])
                {
                    Transform op = tags[i];
                    tags[i] = tags[j];
                    tags[j] = op;
                }
            }

        }
        s1();

    }
    public void s1()
    {
        for (int i = 0; i < tags.Count; i++)
        {
            if (tags[i].CompareTag("Player1"))
                playerPositon = i + 1;
        }
        text.text= playerPositon.ToString()+"/"+tags.Count;
    }
}
