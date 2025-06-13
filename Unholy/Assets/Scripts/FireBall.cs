using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float velocidad = 5f;
    public float tiempoVida = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool haImpactado = false; // Para evitar m�ltiples destrucciones

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.velocity = transform.right * velocidad; // Se mueve en la direcci�n que fue instanciado

        // Comenzamos la cuenta regresiva para destruirlo
        StartCoroutine(DestruirConAnimacion());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !haImpactado) // Evitamos que destruya varias veces
        {
            haImpactado = true;
            Debug.Log("Impacto en el jugador!");

            // Activamos la animaci�n antes de destruir el objeto
            animator.SetTrigger("explosion");

            // Desactivamos el movimiento para que no siga avanzando
            rb.velocity = Vector2.zero;

            // Esperamos a que termine la animaci�n antes de destruirlo
            StartCoroutine(EsperarYDestruir());
        }
    }

    IEnumerator DestruirConAnimacion()
    {
        yield return new WaitForSeconds(tiempoVida);
        animator.SetTrigger("explosion"); // Activa la animaci�n al final del tiempo de vida
        yield return new WaitForSeconds(0.5f); // Esperamos a que termine la animaci�n (ajusta seg�n tu clip)
        Destroy(gameObject);
    }

    IEnumerator EsperarYDestruir()
    {
        yield return new WaitForSeconds(0.5f); // Ajusta esto al tiempo de la animaci�n
        Destroy(gameObject);
    }
}
