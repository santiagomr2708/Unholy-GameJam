using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            //disparar
            Disparar();
        }
    }

    private void Disparar()
    {
        // Instancia la bala
        GameObject nuevaBala = Instantiate(bala, controladorDisparo.position, Quaternion.identity);

        // Determinar dirección basado en el personaje
        float direccionDisparo = transform.localScale.x > 0 ? 1f : -1f;

        // Ajustar la escala de la bala para que coincida con la dirección
        nuevaBala.transform.localScale = new Vector3(direccionDisparo, 1f, 1f);

        // Obtener el Rigidbody2D y mover la bala
        Rigidbody2D rbBala = nuevaBala.GetComponent<Rigidbody2D>();
        rbBala.velocity = new Vector2(direccionDisparo * 5f, 0f);
    }

}
