using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float elapsedTime;
    // Update is called once per frame
    private void Start()
    {
        
        StartCoroutine(abc());
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        textMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public IEnumerator abc()
    {
        yield return new WaitForSeconds(4f);
    }
}
