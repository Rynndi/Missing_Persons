using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string nextScene = "default";

    public void transitionScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void transitionScene()
    {
        SceneManager.LoadSceneAsync(nextScene);    
    }

}