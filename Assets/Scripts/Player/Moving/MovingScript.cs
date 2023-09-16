using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class MovingScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10;

    PhotonView view;

    private Vector2 inputMoving;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
            CameraFollow.InitializeCam(transform);
    }

    void Update()
    {
        if (view.IsMine)
        {
            rb.velocity = inputMoving * speed;
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        inputMoving = context.ReadValue<Vector2>();
    }
}
