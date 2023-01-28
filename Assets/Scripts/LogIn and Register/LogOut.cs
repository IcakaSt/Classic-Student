using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogOut : MonoBehaviour
{
    GameObject userData;

    private void Start()
    {
        userData = GameObject.FindGameObjectWithTag("UserData");
    }

    public void Logout()
    {
        Destroy(userData);
        SceneManager.LoadScene("Log In");
    }

}
