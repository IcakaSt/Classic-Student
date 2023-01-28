using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public Text titleText;
    public Text cancelText;
    public Text okayText;
 
    Animator anim;

    string msgbTitle;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void GenerateMessageBox(string title, string okay, string cancel, string type)
    {
        titleText.text = title;
        cancelText.text = cancel;
        okayText.text = okay;
        msgbTitle = type;
    }

    public void Cancel()
    {  
        this.gameObject.SetActive(false);
        PlayerPrefs.SetInt(msgbTitle, 0);
    }

    public void StopPopUp()
    {anim.SetBool("PopUp", false);}

    public void Okay()
    {
        this.gameObject.SetActive(false);
        PlayerPrefs.SetInt(msgbTitle, 1);
    }

}
