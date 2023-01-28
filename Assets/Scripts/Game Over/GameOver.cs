using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject TestGameObject;
    [SerializeField] private GameObject NameGameObject;

    Test test;
    Score score;

    List<Score> scoresList = new List<Score>();

    public Text points;

    private void Start() //Get the points from the test
    {
        Cursor.visible = true;
        Screen.lockCursor = false;

        TestGameObject = GameObject.FindGameObjectWithTag("Test");
        NameGameObject = GameObject.FindGameObjectWithTag("StudentNames");

        GetTest(TestGameObject.GetComponent<InsertCode>().test);
        GetScore(TestGameObject.GetComponent<InsertCode>().score);
        points.text = "Points: " + score.points + " out of " + test.totalPoints;

        StartCoroutine(InsertNewStudentScore());
    }

    
    IEnumerator InsertNewStudentScore()
    {
        Debug.Log("All the scores are: " + score.points);

        var form = new WWWForm();
        form.AddField("studentName", score.studentName);
        form.AddField("testId", test.testId);
        form.AddField("points", score.points);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/insertscore.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else { Debug.Log("Form upload complete!" + www.downloadHandler.text); }
        }
    }

    public void GetTest(Test GetTest)
    {
        test = GetTest;
    }

    public void GetScore(Score GetScore)
    {
        score = GetScore;
    }
}
