using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject timeMenu;
    public GameObject settingMenu;
    public bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPause){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }
    public void PauseGame(){
        pauseMenu.SetActive(true);
        timeMenu.SetActive(false);
        Time.timeScale=0f;
        isPause=true;
    }
    public void ResumeGame(){
        pauseMenu.SetActive(false);
        timeMenu.SetActive(true);
        Time.timeScale=1f;
        isPause=false;
    }
    public void Setting()
    {
        settingMenu.SetActive(true);
    }
}
