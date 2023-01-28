using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestionType : MonoBehaviour
{
    public string correct = "False";

    public Sprite Wrong;
    public Sprite Correct;

    public char buttonId;

    GameObject InserterGameObject;

    private void Start()
    {
        InserterGameObject = GameObject.FindGameObjectWithTag("ChangeQuestion");

        if (correct== "True")
        {
            this.gameObject.GetComponent<Image>().sprite = Correct;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = Wrong;
        }
    }
    public void ButtonClicked()
    {
        if (correct == "true")
        {
            correct = "False";
            this.gameObject.GetComponent<Image>().sprite = Wrong;
        }
        else 
        { 
            correct = "True";
            this.gameObject.GetComponent<Image>().sprite = Correct;
        }
        buttonId = this.gameObject.name[0];

        InserterGameObject.GetComponent<ChooseQuestions>().ChangeQuestionType(int.Parse(this.gameObject.name.Substring(0, 1)), correct);
    }

    private void Update()
    {
        if (correct == "True")
        {
            this.gameObject.GetComponent<Image>().sprite = Correct;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = Wrong;
        }
    }

}
