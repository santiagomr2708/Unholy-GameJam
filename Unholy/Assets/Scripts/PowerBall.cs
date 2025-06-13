using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBall : MonoBehaviour
{
    public float velocidad = 5f;
    public float tiempoVida = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool haImpactado = false; // Para evitar múltiples destrucciones

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Determinar la dirección de disparo basada en la escala del objeto
        float direccionDisparo = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(direccionDisparo * velocidad, 0f);

        StartCoroutine(DestruirConAnimacion());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !haImpactado) // Evitamos que destruya varias veces
        {
            haImpactado = true;
            Debug.Log("Impacto en el jugador!");

            // Activamos la animación antes de destruir el objeto
            animator.SetTrigger("explosion");

            // Desactivamos el movimiento para que no siga avanzando
            rb.velocity = Vector2.zero;

            // Destruir al jugador
            Destroy(other.gameObject);

            // Esperamos a que termine la animación antes de destruirlo
            StartCoroutine(EsperarYDestruir());
        }
    }

    IEnumerator DestruirConAnimacion()
    {
        yield return new WaitForSeconds(tiempoVida);
        animator.SetTrigger("explosion"); // Activa la animación al final del tiempo de vida
        yield return new WaitForSeconds(0.5f); // Esperamos a que termine la animación (ajusta según tu clip)
        Destroy(gameObject);
    }

    IEnumerator EsperarYDestruir()
    {
        yield return new WaitForSeconds(0.5f); // Ajusta esto al tiempo de la animación
        Destroy(gameObject);
    }
}
