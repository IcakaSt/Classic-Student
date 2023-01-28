using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeQuestionId : MonoBehaviour
{
    GameObject InserterGameObject;

    // Start is called before the first frame update
    void Start()
    {
        InserterGameObject= GameObject.FindGameObjectWithTag("ChangeQuestion");
    }

    public void ChangeId()
    {
        InserterGameObject.GetComponent<ChooseQuestions>().ChangeQuestion(int.Parse(this.gameObject.name) - 1);
        Debug.Log("Clicked button " + this.gameObject.name);
    }
}
