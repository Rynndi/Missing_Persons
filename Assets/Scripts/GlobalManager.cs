using Pathfinding;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    [SerializeField]
    public AILerp seeker;

    [SerializeField]
    public PlayerController player;

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
        GlobalEvents.onPauseInvoked += PauseGame;
        GlobalEvents.onCollected += PersonKilled;
        GlobalEvents.onResumeInvoked += ResumeGame;
        GlobalEvents.pauseButtonClicked += PauseButtonClicked;
        GlobalEvents.resumeButtonClicked += ResumeGameClicked;
    }

    void OnDisable()
    {
        GlobalEvents.onGameStart -= StartGame;
        GlobalEvents.onPauseInvoked -= PauseGame;
        GlobalEvents.onCollected -= PersonKilled;
        GlobalEvents.onResumeInvoked -= ResumeGame;
        GlobalEvents.pauseButtonClicked -= PauseButtonClicked;
        GlobalEvents.resumeButtonClicked -= ResumeGameClicked;
    }

    void StartGame()
    {
        
    }

    void PauseGame()
    {
        seeker.pause();
        player.pause();
    }

    void ResumeGame()
    {
        seeker.resume();
        player.resume();
    }

    void PauseButtonClicked()
    {
        Debug.Log("pause attempted");
        PauseGame();
    }

    void ResumeGameClicked()
    {
        ResumeGame();
    }

    void PersonKilled()
    {
        
    }
}