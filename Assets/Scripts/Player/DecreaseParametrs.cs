using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseParametrs : MonoBehaviour
{
    public Health health;
    public void TakeDamage(int damage)
    {
        health.ChangeHealth(-damage);
    }
}
