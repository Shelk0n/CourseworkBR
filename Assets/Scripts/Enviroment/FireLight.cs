using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLight : MonoBehaviour
{
    private Light2D thisLight;
    public float minValue;
    public float maxValue;
    void Start()
    {
        thisLight = GetComponent<Light2D>();
    }

    void Update()
    {
        if (thisLight.intensity < minValue)
            thisLight.intensity += 0.015f;
        else if (thisLight.intensity > maxValue)
            thisLight.intensity -= 0.015f;
        else
            thisLight.intensity += Random.Range(-0.01f, 0.01f);
    }
}
