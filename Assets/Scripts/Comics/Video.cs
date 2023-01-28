using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Video : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().frame>1 && !this.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying)
        {
            SceneManager.LoadScene("ReadyScene");
        }
    }
}
