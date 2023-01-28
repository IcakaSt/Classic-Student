using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    private void Start()
    {
         //this.gameObject.SetActive(false);
    }
    public void StartLoading()
    {
         this.gameObject.SetActive(true);
    }

    public void StopLoading()
    {
         this.gameObject.SetActive(false);
    }
}
