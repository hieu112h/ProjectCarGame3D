using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

public class ResolutionManage : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    public Slider Brightness;
    public GameObject abc;
    public Light screenLight;
    public Slider musicVol;
    public AudioMixer musicMixer;
    public Toggle FullScreen;
    private Resolution[] resolutions;
    private List<string> resolutionList;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    private int selectResolution;
    public List<Resolution> SelectResolution=new List<Resolution>();
    public bool IsFullScreen;
    // Start is called before the first frame update
    void Start()
    {
        IsFullScreen = true;
        resolutions=Screen.resolutions;
        resolutionList= new List<string>();
        resolutionDropdown.ClearOptions();
        string newRes;
        for(int i = 0; i < resolutions.Length; i++)
        {
            newRes= resolutions[i].width.ToString()+" x " + resolutions[i].height.ToString();
            resolutionList.Add(newRes);
            SelectResolution.Add(resolutions[i]);
        }
        resolutionDropdown.AddOptions(resolutionList);

    }
    public void changeResolution()
    {
        selectResolution=resolutionDropdown.value;
        Screen.SetResolution(SelectResolution[selectResolution].width, SelectResolution[selectResolution].height,IsFullScreen);
    }
    public void changeIsFullScreen()
    {
        IsFullScreen = FullScreen.isOn;
        Screen.SetResolution(SelectResolution[selectResolution].width, SelectResolution[selectResolution].height, IsFullScreen);
    }
    public void ChangeAudioMixer()
    {
        musicMixer.SetFloat("volume",musicVol.value);
    }
    //public void ChangeBright()
    //{
    //    PlayerPrefs.SetFloat("bright",Brightness.value);
    //}
    public void Exit()
    {
        abc.SetActive(false);
        screenLight.intensity= Brightness.value;
        Time.timeScale = 0f;
    }


}
