using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public Transform shootingController;
    public float lineLength;
    public LayerMask playerLayer;
    public bool playerInRange;
    public GameObject enemyProjectile;
    public float timeBetweenProjectiles;
    public float timeSinceLastProjectile;
    public float waitingTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.Raycast(shootingController.position, transform.right, lineLength, playerLayer);

        if (playerInRange)
        {
            if (Time.time > timeBetweenProjectiles + timeSinceLastProjectile)
            {
                timeSinceLastProjectile = Time.time;
                Invoke(nameof(Fire), waitingTime);
            }
        }
    }

    private void Fire()
    {
        Instantiate(enemyProjectile, shootingController.position, shootingController.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingController.position, shootingController.position + transform.right * lineLength);
    }
}
