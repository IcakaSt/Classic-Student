 using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;
public class Inserter : MonoBehaviour
{
    GameObject UserData;
    string result;

    public GameObject LoadingGameObject;
    Test insertTest = new Test();

    private void Start()
    {
        UserData = GameObject.FindGameObjectWithTag("UserData");
    }
    public void InsertData(Test test) //Insert the test into the database
    {
        insertTest = test;
        //    insertTest.userId = UserData.GetComponent<SaveUserData>().UserId.ToString();
        insertTest.userId = "1";
        int total = 0;
        int c = 0;
        foreach (Question quest in test.questions)
        {
            total +=int.Parse(quest.points);
            Debug.Log("TOTAL POINTS " + total.ToString());
            Debug.Log("plus " + quest.points);
        }
        insertTest.totalPoints = total.ToString();
        insertTest.questions = test.questions;
  
        StartCoroutine(Insert());  
    }

    IEnumerator Insert()
    {
       
        LoadingGameObject.SetActive(true);


        var form = new WWWForm();
        form.AddField("test", JsonUtility.ToJson(insertTest));
        Debug.Log("Starting");
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/inserttest.php", form);
        LoadingGameObject.SetActive(true);
        yield return www.SendWebRequest();
        Debug.Log("Trying");
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error");
            Debug.Log(www.error); 
            LoadingGameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Form upload complete!" + www.downloadHandler.text);
            var result = JsonUtility.FromJson<ApiError>(www.downloadHandler.text);
            if (result.errorCode == "0") { Debug.Log("Test made successfuly"); }

            SceneManager.LoadScene("MenuTeacher");
        }
        www.Dispose();
        
    }
}


 