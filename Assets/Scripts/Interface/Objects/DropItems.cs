using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DropItems : MonoBehaviour
{
    public int id;
    PhotonView view;
    public float pickUpRadius = 4.5f;
    public bool isSpell = false;
    public int shieldValue = 0;
    public int healthValue = 0;
    public int manaValue = 0;
    private void OnMouseDown()
    {
        if ((gameObject.transform.position - Camera.main.GetComponent<CameraFollow>().followPoint.position).magnitude < pickUpRadius)
        {
            view.TransferOwnership(PhotonNetwork.LocalPlayer);
            StartCoroutine(WaitingOwnership());
        }
    }
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    IEnumerator WaitingOwnership()
    {
        yield return new WaitUntil(() => view.IsMine);
        if (Inventory.instance.TryToPickSpell(id, isSpell, this))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
