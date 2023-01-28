using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResultButtonsScript : MonoBehaviour
{
    public void ResultsButton(GameObject btn)
    {
        GameObject.Find("ResultManager").GetComponent<ShowTests>().ShowScores(btn.name);
    }

    public void EditButton(GameObject btn)
    {
        GameObject.Find("ResultManager").GetComponent<ShowTests>().ShowEdit(btn.name);
    }
}
