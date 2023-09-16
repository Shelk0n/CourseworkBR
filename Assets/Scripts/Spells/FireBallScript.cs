using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBallScript : MonoBehaviour
{
    public float speed = 10;
    public float lifetime = 0.5f;
    public int damage = 10;
    public float distance = 0.5f;
    public LayerMask whatIsSolid;
    private void Awake()
    {
        gameObject.GetComponent<Animator>().SetBool("Start", true);
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<DecreaseParametrs>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
