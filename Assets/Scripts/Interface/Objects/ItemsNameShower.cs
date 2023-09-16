using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsNameShower : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private void OnMouseEnter()
    {
        text.SetActive(true);
    }
    private void OnMouseExit()
    {
        text.SetActive(false);
    }
}
