using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;

    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool puedeDoblesalto; // Variable para permitir el doble salto
    private Rigidbody2D rb;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal");

        animator.SetFloat("movement", Mathf.Abs(velocidadX));

        // Voltear personaje según dirección
        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        // Mueve el personaje aplicando la velocidad 
        Vector3 posicion = transform.position;
        transform.position = new Vector3(posicion.x + velocidadX * velocidad * Time.deltaTime, posicion.y, posicion.z);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo)
        {
            puedeDoblesalto = true; // Restablecemos la posibilidad de doble salto al tocar el suelo
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enSuelo || puedeDoblesalto) // Permitir salto si está en el suelo o puede hacer doble salto
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Resetear velocidad vertical para evitar saltos inconsistentes
                rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);

                if (!enSuelo) // Si el jugador ya está en el aire, desactivar el doble salto
                {
                    puedeDoblesalto = false;
                }
            }
        }

        animator.SetBool("ensuelo", enSuelo);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }

    void OnDestroy()
    {
        if (SceneTrigger2D.cambiandoEscena) return; // Si estamos cambiando de escena, no hacer nada

        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.MostrarGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager no encontrado!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("El jugador ha sido eliminado por un enemigo!");

            Destroy(gameObject);
            FindObjectOfType<GameOverManager>().MostrarGameOver();
        }
    }


}
