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
        if (player != null) // Verificar si el jugador aún existe
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

                // Verificar que el jugador sigue existiendo antes de disparar
                if (player != null)
                {
                    Disparar();
                }

                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("Jugador fuera de alcance, cambiando a movimiento.");
                animator.SetTrigger("alcanceFuera");
            }
        }
        else
        {
            Debug.LogWarning("El jugador ha sido destruido, el enemigo deja de atacarlo.");
        }
    }

    void Disparar()
    {
        if (player == null) return; // Evita disparar si el jugador ya fue destruido

        GameObject fireBall = Instantiate(fireBallPrefab, firePoint.position, Quaternion.identity);

        // Obtener la dirección hacia el jugador
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Enviar la dirección a `FireBall`
        FireBall fireBallScript = fireBall.GetComponent<FireBall>();
        fireBallScript.SetDirection(direction);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

