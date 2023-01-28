using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReadyScene : MonoBehaviour
{
    private void Start()//Hide mouse cursor 
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Portal")
        {
            SceneManager.LoadScene("Play Scene");
        }
    }
}
