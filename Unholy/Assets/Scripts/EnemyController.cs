using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar al jugador en la escena de manera dinámica
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb.gravityScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) // Verificar si el jugador sigue existiendo
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRadius)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                movement = new Vector2(direction.x, 0);

                enMovimiento = true;
            }
            else
            {
                movement = Vector2.zero;
                enMovimiento = false;
            }

            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - (9.8f * Time.deltaTime)); // Ajusta 9.8 según la gravedad de Unity
            animator.SetBool("enMovimiento", enMovimiento);
        }
        else
        {
            Debug.LogWarning("El jugador ha sido destruido, el enemigo deja de seguirlo."); // Mensaje de depuración
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
