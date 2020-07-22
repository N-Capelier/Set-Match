using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Header("Scene"), SerializeField] 
    private string sceneToGo;

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GoToSceneIn(float time)
    {
        Invoke("GoToSerializedScene", time);
    }
    public void GoToSerializedScene()
    {
        SceneManager.LoadScene(sceneToGo);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NextSceneIn(float time)
    {
        Invoke("NextScene", time);
    }
    
    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void PreviousSceneIn(float time)
    {
        Invoke("PreviousScene", time);
    }
    
    public void QuitGame()
    {
        Debug.LogError("Vous venez de quitter le jeu");
        Application.Quit();
    }
    public void QuitGameIn(float time)
    {
        Invoke("QuitGame", time);
    }
}
