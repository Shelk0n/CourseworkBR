using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChooseInvButton : MonoBehaviour
{
    public delegate void OnSlotChoose(int num);
    public event OnSlotChoose onSlotChooseEvent;
    public int currentSlot;
    private void Start()
    {
        //OldInventory.InitializeChoosing(this);
    }
    public void OnePressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            currentSlot = 1;
            this.onSlotChooseEvent?.Invoke(1);
        }
    }
    public void TwoPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            currentSlot = 2;
            this.onSlotChooseEvent?.Invoke(2);
        }
    }
    public void ThreePressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            currentSlot = 3;
            this.onSlotChooseEvent?.Invoke(3);
        }
    }
    public void FourPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            currentSlot = 4;
            this.onSlotChooseEvent?.Invoke(4);
        }
    }
    public void FivePressed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            currentSlot = 5;
            this.onSlotChooseEvent?.Invoke(5);
        }
    }
}
