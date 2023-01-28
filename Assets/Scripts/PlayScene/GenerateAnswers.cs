using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GenerateAnswers : MonoBehaviour
{
    [Header("Player Objects")]
    [SerializeField] GameObject player;
    public GameObject playerPosition;
    public CinemachineFreeLook cin;
    public GameObject teleportscreen;

    [Header("Test Objects")]
    [SerializeField] private int currectQuestionId = 0;
    [SerializeField] private float fieldSize, answerSize;
    [SerializeField] private GameObject TestGameObject;
    [SerializeField] private GameObject NameGameObject;
    public GameObject answerObject;
    public TextMesh answerText,questionText;
    public Text timerText, questionIdText;
    public GameObject answerPosition;
    private Vector3 position;
    [SerializeField] bool nextQuestion = false;
    [SerializeField] int points = 0;
    [SerializeField] float timer = 5000;
    public GameObject tablet;

    [Header("Classes")] 
    Test test = new Test();
    Score score = new Score();
    List<string> studentsAnswersList = new List<string>();


    void GetTest(Test GetTest)
    {
        test = GetTest;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        NameGameObject = GameObject.FindGameObjectWithTag("StudentNames");
        TestGameObject = GameObject.FindGameObjectWithTag("Test");
        GetTest(TestGameObject.GetComponent<InsertCode>().test);
        timer = int.Parse(test.timer) * 60;
        GenerateDoors();
    }

    // Update is called once per frame
    void Update()
    {
        //Timer
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timer -= Time.deltaTime;
        timerText.text = "Time: " + minutes.ToString() + ":" + seconds.ToString();

        questionIdText.text = "Question: "+(currectQuestionId+1)+"/"+test.numberQuestions;

        /*/    
              if (nextQuestion)
              {
                  currectQuestionId++;
                  if (currectQuestionId + 1 <= int.Parse(test.numberQuestions))
                  {
                      nextQuestion = false;
                      DeleteDoors();
                      GenerateDoors();
                      Teleport();
                  }
                  else {FinishTest();}
              }  
         /*/

        if (timer <= 0){FinishTest();}
    }

    void NextQuestion()
    {
        currectQuestionId++;
        if (currectQuestionId + 1 <= int.Parse(test.numberQuestions))
        {
            nextQuestion = false;
            DeleteDoors();
            GenerateDoors();
            Teleport();
        }
        else { FinishTest(); }
    }

    void DeleteDoors() //Delete all the current portals
    {GameObject.Find("All answers").SetActive(false);}

    void Teleport()
    {
        teleportscreen.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = playerPosition.transform.position; Debug.Log("Teleported");
        player.transform.rotation = playerPosition.transform.rotation; Debug.Log("Rotated");
        cin.gameObject.transform.rotation = playerPosition.transform.rotation;
        cin.transform.position = playerPosition.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        cin.m_XAxis.Value = 200;
    }

    void GenerateDoors() //Generate all the possible answers as gameobjects
    {
        Cursor.visible = false;
        Screen.lockCursor = true;

        GameObject allAnswers;
        allAnswers = new GameObject();
        allAnswers.name = "All answers";

        position = answerPosition.transform.position;

        Vector3 answerSpawnPosition;
        Vector3 textSpawnPosition;

        Debug.Log("The current question is " + currectQuestionId);

        string _questionText = test.questions[currectQuestionId].questionText;
        _questionText = SeparateText(_questionText, 30);

        switch (test.questions[currectQuestionId].openAnswer)
        {
            case "True":
                tablet.SetActive(true);
                questionText.text = "Go to the tablet and answer the question";

                break;
            case "False":

                tablet.SetActive(false);
                questionText.text = test.questions[currectQuestionId].questionText;
                for (int i = 0; i < 4; i++)
                {
                    GameObject answer;
                    TextMesh text;
                    if (test.questions[currectQuestionId].answers[i].answerText != "")
                    {
                        answerSpawnPosition = new Vector3(position.x, 0, position.z + answerSize / 2 + (fieldSize / int.Parse(test.questions[currectQuestionId].answersNumber)) * i);
                        textSpawnPosition = new Vector3(position.x, 17, (position.z + answerSize / 2 + (fieldSize / int.Parse(test.questions[currectQuestionId].answersNumber)) * i) + 7);
                        answer = Instantiate(answerObject, answerSpawnPosition, Quaternion.identity) as GameObject;
                        answer.transform.parent = allAnswers.transform;
                        answer.name = i.ToString();
                        text = Instantiate(answerText, textSpawnPosition, Quaternion.Euler(0, 90, 0)) as TextMesh;
                        text.transform.parent = answer.transform;

                        string _answerText = test.questions[currectQuestionId].answers[i].answerText;
                        _answerText = SeparateText(_answerText, 15);
                        text.text = test.questions[currectQuestionId].answers[i].answerText;
                    }
                }
                break;
        }
    }

    public void TabletSubmit(GameObject tabletScreen)
    {
        studentsAnswersList.Add(tabletScreen.transform.GetChild(1).GetComponent<InputField>().text.ToLower());

        if (tabletScreen.transform.GetChild(1).GetComponent<InputField>().text.ToLower() == test.questions[currectQuestionId].answers[0].answerText.ToLower())
        { points += int.Parse(test.questions[currectQuestionId].points); }

        tabletScreen.transform.GetChild(1).GetComponent<InputField>().text = "";

        tabletScreen.SetActive(false);
        NextQuestion();
    }

    string SeparateText(string text, int limit)
    {
        char[] chars= text.ToCharArray();
        text = "";
        int cal = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            if (cal >= limit && chars[i] == ' ')
            {
                chars[i] = '\n';
                cal = 0;
            }
            else { cal++; }

            text += chars[i];
        }
        return text;
    }

    public void Collide(int name)
    {
        studentsAnswersList.Add(test.questions[currectQuestionId].answers[name].answerText);

        if (test.questions[currectQuestionId].answers[name].correct == "true")
        {
            points += int.Parse(test.questions[currectQuestionId].points);    
        }
        nextQuestion = true;
        NextQuestion();
    }



    void FinishTest()
    {
        SceneManager.LoadScene("GameOver");
        score.points = points.ToString();

        score.studentName = NameGameObject.GetComponent<SaveNames>().names;
        score.testId = test.testId;

        TestGameObject.GetComponent<InsertCode>().score = score;
    }

}
