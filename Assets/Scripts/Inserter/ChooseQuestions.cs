using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;

public class ChooseQuestions : MonoBehaviour
{
    [Header("Main Elements")]
    public GameObject FirstPart;
    public GameObject SecondPart;

    [Header("UI Elements")]
    public Text questionIdText;
    public GameObject questionButton, AllQuestionsButtons, msgb;
    public TMP_Text errorText;
    public Toggle openAnswerCheck;
    [SerializeField] List<GameObject> questionsButtons = new List<GameObject>();

    [Header("Test InputFields")]
    public InputField QuestionInput, PointsInput, TimeInput, TitleInput;
    public InputField[] AnswersInput;
    GameObject toggleButton;

    [SerializeField] public Test test = new Test();

    GameObject SceneManager;
    [SerializeField] int currentId = 0;
    [SerializeField] int questionsnumber = 0;
    [SerializeField] int answersnumber = 0;
    [SerializeField] int timer = 0;
    [SerializeField] bool secondPart = false;

    private void Start()
    {
        toggleButton = GameObject.Find(0 + "TypeButton");
        openAnswerCheck.isOn = false;

        CreateQuestion();
        ShowInformation(0);
        msgb.SetActive(false);
        SceneManager = GameObject.FindGameObjectWithTag("SceneManager");

        errorText.text = "";
    }
    private void Update()
    {  
        //Change between parts of making a test
        if (secondPart) { FirstPart.SetActive(false); SecondPart.SetActive(true);}
        else { FirstPart.SetActive(true); SecondPart.SetActive(false); }

        if (PlayerPrefs.GetInt("deletion") == 1) //delete a question
        {
            PlayerPrefs.SetInt("deletion", 0);
            test.questions.Remove(test.questions[currentId]);
            GameObject ButtonToDelete = questionsButtons[currentId];
            ButtonToDelete.SetActive(false);
            questionsButtons.Remove(questionsButtons[currentId]);

            questionsnumber--;

            for (int i = 0; i < questionsnumber; i++)
            {
                questionsButtons[i].name = (i + 1).ToString(); 
                questionsButtons[i].GetComponentInChildren<Text>().text = "Q" + (i + 1);
            }
            ShowInformation(0);
        }

        if (PlayerPrefs.GetInt("leaving") == 1) //Going back to the main menu
        {
            PlayerPrefs.SetInt("leaving", 0);
            SceneManager.GetComponent<ChangeScene>().SceneChanger("MenuTeacher");
        }
        if (PlayerPrefs.GetInt("store") == 1)   //Store all the data into the realtime database
        {
            PlayerPrefs.SetInt("store", 0);
            test.timer = TimeInput.text;

            SaveInformation();
            foreach (Question question in test.questions)
            {
                foreach (Answer answer in question.answers.ToList())
                {
                    if (string.IsNullOrEmpty(answer.answerText))
                    { question.answers.ToList().Remove(answer); }
                }
            }
            this.gameObject.GetComponent<Inserter>().InsertData(test);
        }

        if (!openAnswerCheck.isOn)
        {
            AnswersInput[1].gameObject.SetActive(true);
            AnswersInput[2].gameObject.SetActive(true);
            AnswersInput[3].gameObject.SetActive(true);
            toggleButton.SetActive(true);
        }
        else 
        { 
            AnswersInput[1].gameObject.SetActive(false);
            AnswersInput[2].gameObject.SetActive(false);
            AnswersInput[3].gameObject.SetActive(false);
            toggleButton.SetActive(false);
        }
    }
    void SaveInformation() //Save the information of the previous button
    {
        test.questions[currentId].questionText = QuestionInput.text;
        for (int i = 0; i <= 3; i++)
        {
            test.questions[currentId].answers[i].answerText = AnswersInput[i].text;
        }
        if (!string.IsNullOrEmpty(PointsInput.text))
        {
            test.questions[currentId].points = PointsInput.text;
        }

        answersnumber = 0;

        if (openAnswerCheck.isOn) { answersnumber = 1; test.questions[currentId].openAnswer = "True"; test.questions[currentId].answers[0].correct = "True"; }
        else
        {
            test.questions[currentId].openAnswer = "False";

            foreach (Answer answer in test.questions[currentId].answers)
            {
                if (!string.IsNullOrEmpty(answer.answerText))
                {
                    answersnumber++;
                }
            }
        }

        test.questions[currentId].answersNumber = answersnumber.ToString();
        int.TryParse(TimeInput.text, out timer);
        test.timer = timer.ToString();
        test.title = TitleInput.text;
        test.numberQuestions = questionsnumber.ToString();
    }

    void ShowInformation(int ID)
    {
        QuestionInput.text = test.questions[ID].questionText;

        if (test.questions[ID].openAnswer == "True") 
        {
            openAnswerCheck.isOn = true; 
            toggleButton.SetActive(false);
            AnswersInput[1].gameObject.SetActive(false);
            AnswersInput[2].gameObject.SetActive(false);
            AnswersInput[3].gameObject.SetActive(false);
        } 
        else 
        { 
            openAnswerCheck.isOn = false;
            toggleButton.SetActive(true); 
            AnswersInput[1].gameObject.SetActive(true);
            AnswersInput[2].gameObject.SetActive(true);
            AnswersInput[3].gameObject.SetActive(true);
        }

        if (test.questions[ID].openAnswer != "True")
        {
            for (int i = 0; i <= 3; i++)
            {
                AnswersInput[i].text = test.questions[ID].answers[i].answerText;
                GameObject.Find(i.ToString() + "TypeButton").GetComponent<QuestionType>().correct = test.questions[ID].answers[i].correct;
            }
        }
        else {
            test.questions[ID].answers[0].correct = "True";

            for (int i = 1; i <= 3; i++)
            {
                test.questions[ID].answers[i].correct = "False";
            }
        }

        questionIdText.text = "Q" + (ID + 1).ToString();
        if (test.questions[ID] != null){ PointsInput.text = test.questions[ID].points; }
    }
    public void ChangeQuestion(int id)  //Change the questions which is changing
    {
        SaveInformation();
        currentId = id;
        ShowInformation(id);
    }
    public void CreateQuestion() //Add new button with a question id and add the question to the dictionary
    {
        questionsnumber++;
        Debug.Log("The current number of the questions is "+questionsnumber);

        GameObject buttonSpwan;
        buttonSpwan = Instantiate(questionButton, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        buttonSpwan.transform.parent = AllQuestionsButtons.transform;
        buttonSpwan.GetComponentInChildren<Text>().text = "Q" + questionsnumber.ToString();
        buttonSpwan.name = questionsnumber.ToString();
        questionsButtons.Add(buttonSpwan);

        Question question = new Question();
        question.answers = new List<Answer>();
        for (int i = 0; i < 4; i++)
        {
            question.answers.Add(new Answer());
        }
        test.questions.Add(question);
    }
    public void ChangeQuestionType(int answerId, string correct)
    {
        test.questions[currentId].answers[answerId].correct = correct;
    }
    public void DeleteQuestion() // Delete the current question
    {
        if (!msgb.active && questionsnumber > 1)
        {
            msgb.SetActive(true);
            msgb.GetComponent<MessageBox>().GenerateMessageBox("Do you want to delete question " + (currentId + 1) + "?", "Yes", "No", "deletion");
        }
    }
    public void GoBack() //leave the page without saving
    {
        if (!secondPart)
        {
            if (!msgb.active)
            {
                msgb.SetActive(true);
                msgb.GetComponent<MessageBox>().GenerateMessageBox("Do you want to leave this window without saving the test?", "Yes", "No", "leaving");
            }
        }
        else { secondPart = false; }
    }
    public void Next()  // Insert the data to Firebase
    {
        if (secondPart)
        {
            SaveInformation();
            if (!msgb.active && !ErrorCheck())
            {
                msgb.SetActive(true);
                msgb.GetComponent<MessageBox>().GenerateMessageBox("Do you want to finish the test?", "Yes", "No", "store");
            }
        }
        else { secondPart = true; }
        
    }
    bool ErrorCheck()
    {
        bool error = false;
        int questid = 0;
        foreach (Question question in test.questions)
        {
            if (string.IsNullOrEmpty(question.questionText))
            {
                errorText.text = "All the questions must have a text!";
                error = true;
                break;
            }
            if (int.Parse(question.answersNumber) < 2 && question.openAnswer=="False")
            {
                errorText.text = "All the questions must have at least two answers with a text!";
                error = true;
                break;
            }

            if (string.IsNullOrEmpty(question.points))
            {
                errorText.text = "All the questions must have points!";
                error = true;
                break;
            }

            error = false;
            if (test.questions[questid].answers.Any(q => q.correct == "True"))
            {
                errorText.text = "";
                error = false;
            }
            else
            {
                errorText.text = "Every question must has at least one correct answer";
                error = true;
            }        
            questid++;
        }
        return error;
    }
}
