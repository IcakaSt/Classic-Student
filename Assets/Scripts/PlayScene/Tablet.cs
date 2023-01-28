using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    public GameObject tabletScreen;

    private void Start()
    {
        tabletScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tabletScreen.SetActive(true);
            Cursor.visible = true;
            Screen.lockCursor = false;
        }
    }

}
