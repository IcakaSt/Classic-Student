using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveNames : MonoBehaviour
{
    public TMP_InputField namesText;
    public string names;
    public void SaveName()
    {
        names = namesText.text;
        if (!string.IsNullOrEmpty(names))
        {
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("TestCode");
        }
    }
}
