using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth)) ;
        {
            playerHealth.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}