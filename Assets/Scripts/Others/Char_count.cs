using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Char_count : MonoBehaviour
{
    public InputField inputField;

    Text textLimit;
    // Start is called before the first frame update
    void Start()
    {
        textLimit = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textLimit.text = inputField.text.Length+"/" + inputField.characterLimit;
    }
}
