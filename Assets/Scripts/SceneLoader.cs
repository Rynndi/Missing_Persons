using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string nextScene = "default";

    public void transitionScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void transitionScene()
    {
        SceneManager.LoadScene(nextScene);    
    }

}