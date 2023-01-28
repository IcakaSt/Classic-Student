using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Text;

public enum RegisterType { student,teacher}

public class Register : MonoBehaviour
{
  
    public TMP_InputField email, password,repeatPassword;
    public Text errorText;
    public RegisterType type;

   // [SerializeField] bool passChecked = false;
    [SerializeField] bool registrated = false;
    [SerializeField] string msg;

   // public GameObject LoadingGameObject;

    User user = new User();
    private void Start()
    {
        errorText.text = "";
        //   LoadingGameObject = GameObject.FindWithTag("Loading");
        //  LoadingGameObject.SetActive(false);
    }

    private void Update()
    {
        if (registrated)
        { SceneManager.LoadScene("Log In");       }

    }
    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
          + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    void GetErrorMessage(string errorCode) { errorText.text = errorCode.ToString(); }
    bool PasswordCheck() => repeatPassword.text == password.text;
    bool EmailCkeck() => email.text != null ? Regex.IsMatch(email.text, MatchEmailPattern) : false;

    public void CallRegister()
    {
        StartCoroutine(Registration());
    }
    IEnumerator Registration()
    {
        if (PasswordCheck())
        {
            if (EmailCkeck())
            {
                var form = new WWWForm();
                form.AddField("email", email.text);
                form.AddField("password", password.text);
                using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/register.php", form))
                {
                    yield return www.SendWebRequest(); 
                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Debug.Log("Form upload complete!" + www.downloadHandler.text);
                        var result = JsonUtility.FromJson<ApiError>(www.downloadHandler.text);
                        Debug.Log(result.errorCode);
                    }
                }
            }
            else { GetErrorMessage("Email is invalid"); }
        }
        else { GetErrorMessage("Passwords do not match");  }

    }


 




}
