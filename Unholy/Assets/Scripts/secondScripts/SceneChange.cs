using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger2D : MonoBehaviour
{
    public string sceneToLoad;
    public static bool cambiandoEscena = false; // Indicador de cambio de escena

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cambiandoEscena = true; // Marcar que estamos cambiando de escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}