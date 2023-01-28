using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void SceneChanger(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
