using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    public string answerId;
    public string questionId;
    public string answerText;
    public string correct;
    public string openAnswer;
}

[System.Serializable]
public class Question
{
    public string questionId; 
    public string testId;
    public string openAnswer = "False";
    public string answersNumber;
    public string points;
    public string questionText;
    public List<Answer> answers;
}

[System.Serializable]
public class Test
{
    public string testId="0";
    public string userId;
    public string numberQuestions;
    public string title;
    public string totalPoints;
    public string timer;
    public string code;
    public List<Question> questions;
}

[System.Serializable]
public class Score
{
    public string scoreId;
    public string studentName;
    public string testId;
    public string points;
}

[System.Serializable]
public class StudentsAnswer
{
    public string userId;
    public string questionId;
    public string answerText;
}

[System.Serializable]
public class AllTest
{
    public Test[] tests;
}

[System.Serializable]
public class AllScore
{
    public Score[] scores;
}

class ApiError
{
    public string errorCode;
}

class ApiLogin
{
    public string errorCode;
    public string userId;
}



[System.Serializable]
public class Skin
{
    public GameObject module;
    public RuntimeAnimatorController anim;
}

[System.Serializable]
public class User
{
    public string email;
    public string password;
}













