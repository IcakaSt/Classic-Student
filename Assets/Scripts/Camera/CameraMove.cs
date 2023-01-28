using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 target_Offset;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            this.transform.position = target.position + target_Offset;
        }
    }
}
