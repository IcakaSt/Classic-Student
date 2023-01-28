using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class InsertCode : MonoBehaviour
{
    [Header("Menu Objects")]
    public TMP_InputField CodeInputField;
    GameObject ErrorText;

    [Header("Classes")]
    public Test test;
    public List<Question> questions;
    public List<List<Answer>> answers;
    public Score score;

    [Header("Others")]
    public GameObject LoadingGameObject;

    private void Start()
    {
        ErrorText = GameObject.FindGameObjectWithTag("Error Text");
        ErrorText.gameObject.GetComponent<TMP_Text>().text = " ";

        CodeInputField.Select();
        CodeInputField.ActivateInputField();

        LoadingGameObject = GameObject.FindWithTag("Loading");
        LoadingGameObject.gameObject.SetActive(false);
    }
    public void StartGame() //Check if the code exist and start the game
    {      
        string code = CodeInputField.text;
        StartCoroutine(FindTest());
    }

    IEnumerator FindTest()
    {
        LoadingGameObject.SetActive(true);

        var form = new WWWForm();
        form.AddField("code", CodeInputField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/gettest.php", form))
        {
            LoadingGameObject.SetActive(true);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error); ErrorText.gameObject.GetComponent<TMP_Text>().text = "The code is wrong";
                LoadingGameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (!string.IsNullOrEmpty(www.downloadHandler.text))
                {
                    Debug.Log("Test: " + www.downloadHandler.text);
                    test = JsonUtility.FromJson<Test>(www.downloadHandler.text);

                    Debug.Log("Test found: " + test.testId);
                    Debug.Log("Test found: " + JsonUtility.ToJson(test));


                    DontDestroyOnLoad(this.gameObject);
                    SceneManager.LoadScene("Play Scene");
                }
                else { ErrorText.gameObject.GetComponent<TMP_Text>().text = "The test code is wrong"; }

            }
        }
    }
}
