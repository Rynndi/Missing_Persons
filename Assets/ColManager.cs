using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class ColManager : MonoBehaviour
{
    public int ColCount;
    public Text ColText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColText.text = ColCount.ToString();
    }

    // private void Condition()
    // {
    //     if (ColCount == 0)
    //     {
    //         SceneManager.LoadScene(1);
    //     }
    // }
}
