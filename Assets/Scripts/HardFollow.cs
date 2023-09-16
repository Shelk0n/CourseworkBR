using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardFollow : MonoBehaviour
{
    private Transform pos;
    public void Initializing(Transform follow)
    {
        pos = follow;
    }
    void Update()
    {
        if (pos != null)
        {
            transform.position = pos.position;
            transform.rotation = pos.rotation;
        }
    }
}
