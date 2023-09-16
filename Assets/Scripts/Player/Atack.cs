using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Atack : MonoBehaviour
{
    private Inventory inventory = Inventory.instance;
    public BulletCreate bulletCreate;
    public void IsQPressed(InputAction.CallbackContext context)
    {
        bool isPressing; float temp = context.ReadValue<float>();
        if (temp > 0) isPressing = true; else isPressing = false;
        if (inventory.items[1] != null)
            bulletCreate.Distributor(inventory.items[1].thisItemId, new Vector2(Input.mousePosition.x, Input.mousePosition.y), isPressing);
    }
    public void IsEPressed(InputAction.CallbackContext context)
    {
        bool isPressing; float temp = context.ReadValue<float>();
        if (temp > 0) isPressing = true; else isPressing = false;
        if (inventory.items[0] != null)
            bulletCreate.Distributor(inventory.items[0].thisItemId, new Vector2(Input.mousePosition.x, Input.mousePosition.y), isPressing);
    }
    private void Update()
    {

    }
}
