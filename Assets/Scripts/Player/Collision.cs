using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] bool collidedTablet = false;

    public GameObject AnswerGenerator;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Portal")
        {
            AnswerGenerator.GetComponent<GenerateAnswers>().Collide(int.Parse(col.gameObject.name));
        }

        if (col.gameObject.tag == "Tablet")
        {
            collidedTablet = true;
            //AnswerGenerator.GetComponent<GenerateAnswers>().Collide(int.Parse(col.gameObject.name));
        }
    }
}
