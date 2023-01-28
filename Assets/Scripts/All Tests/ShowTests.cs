using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class ShowTests : MonoBehaviour
{
    public Button TestsButton;
    private GameObject ScrollArea;

    public GameObject ShowResults;
    public GameObject StudentInformation;

    [SerializeField] private List<Button> TestButtons=new List<Button>();

    AllTest allTests;
    AllScore allScores;

    GameObject UserData;
    // Start is called before the first frame update
    void Start()
    {
        UserData = GameObject.FindGameObjectWithTag("UserData");
        ScrollArea = GameObject.Find("Content");
        ShowResults.SetActive(false);

        StartCoroutine(FindAllTests());
    }

    IEnumerator FindAllTests() //Get the list with all tests
    {
        var form = new WWWForm();
        // form.AddField("userId", UserData.GetComponent<SaveUserData>().UserId.ToString());
        form.AddField("userId", "1");

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/getalltests.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!" + www.downloadHandler.text);
                if (www.downloadHandler.text != null)
                {
                    allTests = JsonUtility.FromJson<AllTest>(www.downloadHandler.text);
                    Debug.Log("Tests found");
                }
            }
        }

        foreach (Test test in allTests.tests)
        {
            ShowAllResults(test.title,test.code, test.testId);
        }
    }

    IEnumerator GetScores(string testId) //Get scores of the clicked test
    {
        AllScore scores;
        var secondForm = new WWWForm();
        secondForm.AddField("testId", testId);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/classicstudent/getscore.php", secondForm))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete! " + www.downloadHandler.text);
                if (www.downloadHandler.text != "error")
                {
                    scores = JsonUtility.FromJson<AllScore>(www.downloadHandler.text);
                    allScores = scores;
                    Debug.Log("Scores found");

                    if (allScores != null)
                    {
                        ShowResults.SetActive(true);
                        ShowResults.transform.InverseTransformPoint(0, 0, 0);
                        foreach (Score score in allScores.scores)
                        {
                            GameObject StudentInfo;
                            StudentInfo = Instantiate(StudentInformation, new Vector3(0, 0, 0), Quaternion.identity);
                            StudentInfo.transform.SetParent(ShowResults.transform.GetChild(1), false);

                            string totalPoints = "0";

                            StudentInfo.transform.GetChild(0).GetComponent<Text>().text = score.studentName;

                            foreach (Test test in allTests.tests)
                            {
                                if (test.testId == testId)
                                { totalPoints = test.totalPoints; }
                            }
                            StudentInfo.transform.GetChild(1).GetComponent<Text>().text = score.points.ToString() + " / " + totalPoints;

                            Debug.Log(score.studentName);
                            Debug.Log(score.points.ToString());

                        }
                        Debug.Log("You have clicked the button with testId " + testId);
                    }
                    else { Debug.Log("No results"); }
                }
            }
        }
    }

    void ShowAllResults(string title,string code, string buttonname) //Open the list with all results
    {
        Button ResultButton;
        ResultButton = Instantiate(TestsButton, new Vector3(0, 0, 0), Quaternion.identity);
        ResultButton.transform.SetParent(ScrollArea.transform, false);
        ResultButton.transform.GetChild(0).GetComponent<Text>().text = title;
        ResultButton.transform.GetChild(1).GetComponent<Text>().text = code;
        ResultButton.name = buttonname.ToString();
        TestButtons.Add(ResultButton);
    }

    public void ShowScores(string buttonName)
    {
        StartCoroutine(GetScores(buttonName));          
    }

    public void ShowEdit(string buttonName)
    {
        StartCoroutine(GetScores(buttonName));
    }

    public void HideResults()
    {
        foreach (Transform child in ShowResults.transform.GetChild(1).transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        ShowResults.SetActive(false);
    }
}
