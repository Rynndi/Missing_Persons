using Pathfinding;
using UnityEditor.MPE;
using UnityEngine;
using Yarn.Unity;

public class GlobalManager : MonoBehaviour
{
    [SerializeField]
    public AILerp seeker;

    [SerializeField]
    public PlayerController player;

    [SerializeField]
    public DialogueRunner dialogue;


    private bool dead = false;

    void OnEnable()
    {
        GlobalEvents.onGameStart += StartGame;
        GlobalEvents.onPauseInvoked += PauseGame;
        GlobalEvents.onCollected += Collected;
        GlobalEvents.onResumeInvoked += ResumeGame;
        GlobalEvents.pauseButtonClicked += PauseButtonClicked;
        GlobalEvents.resumeButtonClicked += ResumeGameClicked;
        GlobalEvents.onDeath += deathOccurred;
    }

    void OnDisable()
    {
        GlobalEvents.onGameStart -= StartGame;
        GlobalEvents.onPauseInvoked -= PauseGame;
        GlobalEvents.onCollected -= Collected;
        GlobalEvents.onResumeInvoked -= ResumeGame;
        GlobalEvents.pauseButtonClicked -= PauseButtonClicked;
        GlobalEvents.resumeButtonClicked -= ResumeGameClicked;
        GlobalEvents.onDeath -= deathOccurred;
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

    void Collected()
    {
        if (player.count == 0)
        {
            dialogue.StartDialogue("Collectible3");
        }
        else if (6 - player.count > 3)
        {
            dialogue.StartDialogue("Collectible2");
        }
        else
        {
            dialogue.StartDialogue("Collectible");
        }
    }

    public void InvokePause()
    {
        GlobalEvents.TriggerPauseInvoked();
    }
    public void InvokeResume()
    {
        if (player.count == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }
        if(dead)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("InkScene");
        }
        GlobalEvents.TriggerResumeInvoked();
    }

    public void deathOccurred()
    {
        dialogue.StartDialogue("Death");
        dead = true;
    }
}