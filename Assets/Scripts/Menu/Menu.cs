using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public TMP_Text userNameText;
    Firebase.Auth.FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {
    //    user = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
     //   userNameText.text = user.DisplayName;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
