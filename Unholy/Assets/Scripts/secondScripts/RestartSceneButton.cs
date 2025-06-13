using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneButton : MonoBehaviour
{
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}