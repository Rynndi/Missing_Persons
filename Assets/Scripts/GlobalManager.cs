using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GlobalEvents.onGameStart += StartGame;
        GlobalEvents.onPauseClicked += PauseGame;
        GlobalEvents.onPersonKilled += PersonKilled;
    }

    void OnDisable()
    {
        GlobalEvents.onGameStart -= StartGame;
        GlobalEvents.onPauseClicked -= PauseGame;
        GlobalEvents.onPersonKilled -= PersonKilled;
    }

    void StartGame()
    {
        
    }

    void PauseGame()
    {

    }

    void PersonKilled()
    {
        
    }
}