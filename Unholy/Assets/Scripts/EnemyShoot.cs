using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject fireBallPrefab;
    public Transform firePoint; // Punto desde donde dispara el enemigo
    public float detectionRadius = 5.0f;
    public float fireRate = 1.5f; // Tiempo entre disparos

    private Animator animator;
    private float nextFireTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius && Time.time >= nextFireTime)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-2, 2, 2);
            }

            // Cambiar animación
            animator.SetTrigger("disparar");
            Debug.Log("Animación disparando activada!");
            // El enemigo detecta al jugador y dispara
            Disparar();
            nextFireTime = Time.time + fireRate; // Configura el siguiente disparo
        }
        else if (distanceToPlayer >= detectionRadius) // Cuando el jugador se aleja
        {
            Debug.Log("Jugador fuera de alcance, cambiando a movimiento.");
            animator.SetTrigger("alcanceFuera"); // Activa la animación de movimiento
        }

    }

    void Disparar()
    {
        // Instancia la bola de fuego en el punto de disparo
        GameObject fireBall = Instantiate(fireBallPrefab, firePoint.position, Quaternion.identity);

        // Añade movimiento a la bola de fuego hacia el jugador
        Rigidbody2D rbFireBall = fireBall.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - firePoint.position).normalized;
        rbFireBall.velocity = direction * 5f; // Velocidad de la bola de fuego
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

