using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InventorySpell : MonoBehaviour
{
    public int thisItemId;
    public int socket;
    public GameObject pickableVariation;
    public float dropRadius = 4;
    public bool isSpell = false;
    public int shieldValue = 0;
    public int healthValue = 0;
    public int manaValue = 0;
    public void OnSpellDrag()
    {
        transform.position = Input.mousePosition;
    }   
    public void OnSpellDrop()
    {
        socket = Inventory.instance.TryToDropSpell(transform, this, socket, isSpell);
        if(socket == -1)
        {
            PhotonNetwork.Instantiate(pickableVariation.name, Camera.main.GetComponent<CameraFollow>().followPoint.position + Vector3.Lerp(new Vector3(0,0,0),
                (Camera.main.ScreenToWorldPoint(transform.position) - Camera.main.GetComponent<CameraFollow>().followPoint.position + new Vector3(0,0,8)).normalized * dropRadius,
                ((Camera.main.ScreenToWorldPoint(transform.position) - Camera.main.GetComponent<CameraFollow>().followPoint.position) + new Vector3(0, 0, 8)).magnitude/dropRadius), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
