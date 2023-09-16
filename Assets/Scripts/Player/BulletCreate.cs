using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletCreate : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject fireBallWaiting;
    public Transform shotPoint;
    private GameObject creatingSpell;

    PhotonView view;

    [SerializeField]private bool isPress;
    public void Distributor(int spellID, Vector2 mousePos, bool isPressing)
    {
        if (view.IsMine)
        {
            if (spellID == 1)
                FireBall(isPressing);
            isPress = isPressing;
        }
    }
    private void FireBall(bool isPressing)
    {
        if (isPressing)
        {
            isPressing = true;
            creatingSpell = PhotonNetwork.Instantiate(fireBallWaiting.name, shotPoint.position, transform.rotation);
            creatingSpell.GetComponent<Animator>().SetBool("StartCreating", true);
            creatingSpell.GetComponent<HardFollow>().Initializing(shotPoint);
        }
        else
        {
            if (creatingSpell.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitingFireBall"))
            {
                PhotonNetwork.Instantiate(fireBall.name, shotPoint.position, transform.rotation);
            }
            creatingSpell.GetComponent<Animator>().SetBool("StartCreating", false);
            PhotonNetwork.Destroy(creatingSpell);
            isPressing = false;
        }
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
    }
}
