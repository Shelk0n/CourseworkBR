using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasItemsName : MonoBehaviour
{
    [SerializeField] private GameObject text;
    public void MouseEnter()
    {
        text.SetActive(true);
    }
    public void MouseLeave() 
    {
        text.SetActive(false);
    }
}
