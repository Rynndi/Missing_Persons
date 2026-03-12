using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void transitionScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

}