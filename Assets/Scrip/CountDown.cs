using System.Collections;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{  
    public TMP_Text countDownText;
    public int countDownCount;
    public GameObject countDownCanvas;
    public GameObject countDownFninsh;   
    private void Start()
    {
        Time.timeScale = 1;
        countDownCount = 3;
        StartCoroutine(StartCountDown());
    }        
    IEnumerator StartCountDown()
    {
        while (countDownCount > 0)
        {
            countDownText.text = countDownCount.ToString();
            yield return new WaitForSeconds(1f);
            countDownCount--;
        }
        countDownText.text = "go";      
        countDownCanvas.SetActive(false);
    }   
}
