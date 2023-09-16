using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followPoint;
    public static void InitializeCam(Transform trans)
    {
        Camera.main.GetComponent<CameraFollow>().followPoint = trans;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followPoint.position + new Vector3(0, 0, -10), Time.deltaTime*4);
    }
}
