using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float velocidad = 5f;
    public float tiempoVida = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool haImpactado = false;
    private Vector2 direccion = Vector2.right; // Dirección inicial

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Aplicar movimiento en la dirección correcta
        rb.velocity = direccion * velocidad;

        StartCoroutine(DestruirConAnimacion());
    }

    public void SetDirection(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion; // Asigna la dirección desde `EnemyShoot`
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !haImpactado)
        {
            haImpactado = true;
            Debug.Log("Impacto en el jugador!");

            animator.SetTrigger("explosion");
            rb.velocity = Vector2.zero;
            Destroy(other.gameObject);

            StartCoroutine(EsperarYDestruir());
        }
    }

    IEnumerator DestruirConAnimacion()
    {
        yield return new WaitForSeconds(tiempoVida);
        animator.SetTrigger("explosion");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator EsperarYDestruir()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
