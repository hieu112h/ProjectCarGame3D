using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class LoginPlayfab : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField emailLoginInput;
    [SerializeField] TMP_InputField passWordLoginInput;
    [SerializeField] GameObject loginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField userNameRegisterInput;
    [SerializeField] TMP_InputField emailRegisterInput;
    [SerializeField] TMP_InputField passWordRegisterInput;
    [SerializeField] GameObject registerPage;

    [Header("Recovery")]
    [SerializeField] TMP_InputField emailRecovery;
    [SerializeField] GameObject recoveryPage;




    //[SerializeField] GameObject welcome;
    //[SerializeField] TMP_Text textWelcome;

    //public List<carObject> carObjects;
    //public List<string> id;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = userNameRegisterInput.text,
            Email = emailRegisterInput.text,
            Password = passWordRegisterInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginInput.text,
            Password = passWordLoginInput.text,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile=true,
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSucces,OnError);
    }
    public void Recovery()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailRecovery.text,
            TitleId = "122A9",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request,OnRecoverySucces,OnErrorRcovery);
    }

    private void OnErrorRcovery(PlayFabError error)
    {
        MessageText.text = "Not email found";
    }

    private void OnRecoverySucces(SendAccountRecoveryEmailResult result)
    {
        OpenLoginPage();
        MessageText.text = "Recovery Maill Sent";
    }

    private void OnLoginSucces(LoginResult result)
    {
        string name = null;

        if (result.InfoResultPayload != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            
        }
        //welcome.SetActive(true);
        //loginPage.SetActive(false);
        PlayerPrefs.SetString("UserName",name);
        
        StartCoroutine(LoadNextScence());
    }

    private void OnError(PlayFabError error)
    {
        MessageText.text=error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnRegisterSucces(RegisterPlayFabUserResult result)
    {
        MessageText.text = "New Account Is Created";
        OpenLoginPage();
    }

    public void save(List<carObject> cars,List<string> Idcar)
    {
        foreach (var item in cars) {
            Idcar.Add(item.ID);
        }
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"CarId",JsonUtility.ToJson(Idcar) }
            }
        };
        PlayFabClientAPI.UpdateUserData(request,OnDataSend,OnError);
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Succes");
    }


    public void get()
    {
        
    }

    public void OpenLoginPage()
    {
        loginPage.SetActive(true);
        registerPage.SetActive(false);
        recoveryPage.SetActive(false);
        
    }
    public void OpenRegisterPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        recoveryPage.SetActive(false);
        
    }
    public void OpenRecoveryPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(false);
        recoveryPage.SetActive(true);
        
    }
    IEnumerator LoadNextScence()
    {
        yield return new WaitForSeconds(3);
        MessageText.text = "Login in";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
