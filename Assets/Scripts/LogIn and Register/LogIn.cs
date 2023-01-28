using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LogIn : MonoBehaviour
{
    public TMP_InputField email, password;

    [SerializeField] string Email;
    [SerializeField] string Password;

    public Text errorText;
    public GameObject SceneChanger;
    public GameObject LoadingGameObject;

    GameObject UserData;

    //FirebaseUser User;

    private void Start()
    {
        errorText.text = "";
        LoadingGameObject = GameObject.FindWithTag("Loading");
        LoadingGameObject.SetActive(false);
        UserData = GameObject.FindGameObjectWithTag("UserData");
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }
    

    IEnumerator Login()
    {
        LoadingGameObject.SetActive(true);
        Email = email.text;
        Password = password.text;

        var form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("password", password.text);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/login.php", form))
        {
            LoadingGameObject.SetActive(true);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error); GetErrorMessage("The details are wrong");
                LoadingGameObject.SetActive(false);
            }
            else
            {
                var result = JsonUtility.FromJson<ApiLogin>(www.downloadHandler.text);
                Debug.Log("Form upload complete!" + result.errorCode);

                if (result.errorCode == "0") { Debug.Log("Logged in - " + result.userId); SceneManager.LoadScene("MenuTeacher"); SaveData(int.Parse(result.userId)); }
              
                if(result.errorCode == "4")
                {
                    Debug.Log(www.error); GetErrorMessage("The details are wrong");
                    LoadingGameObject.SetActive(false);
                }
            }
        }     
    }
    void GetErrorMessage(string errorCode)
    {
        errorText.text = errorCode.ToString();
    }

    void SaveData(int ID)
    {
        UserData.GetComponent<SaveUserData>().UserId = ID;
        DontDestroyOnLoad(UserData);
    }
}
