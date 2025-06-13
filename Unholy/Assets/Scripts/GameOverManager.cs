using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Asigna el Canvas en el Inspector

    void Start()
    {
        gameOverUI.SetActive(false); // Oculta Game Over al inicio
    }

    public void MostrarGameOver()
    {
        gameOverUI.SetActive(true); // Activa la pantalla Game Over
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}

