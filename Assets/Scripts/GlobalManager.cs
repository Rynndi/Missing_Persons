using Pathfinding;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (SceneManager.GetActiveScene().name == "InkScene" && StateManager.Instance.phase == 1)
        {
            dialogue.StartDialogue("KillerIntro");
        }
        if (SceneManager.GetActiveScene().name == "InkScene" && StateManager.Instance.phase == 2)
        {
            dialogue.StartDialogue("CollectedIntro");
        }
        if (SceneManager.GetActiveScene().name == "BackGarden" && StateManager.Instance.phase == 2)
        {
            dialogue.StartDialogue("Outro");
        }
    }

    void PauseGame()
    {
        if (seeker != null)
        {
            seeker.pause();
        }
        if (player != null)
        {
            player.pause();
        }
    }

    void ResumeGame()
    {
        if (seeker != null)
        {
            seeker.resume();
        }
        if (player != null)
        {
            player.resume(); 
        }
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
        GlobalEvents.TriggerResumeInvoked();
        if (player.count == 0)
        {
            StateManager.Instance.phase = 2;
            StateManager.Instance.storedPos = player.gameObject.transform.position;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        if (dead)
        {
            dialogue.Stop();
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            dead = false;
        }
        if (player.nextScene && StateManager.Instance.phase == 1)
        {
            dialogue.Stop();
            player.nextScene = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("InkScene");
        }
        else if (player.nextScene && StateManager.Instance.phase == 2)
        {
            dialogue.Stop();
            UnityEngine.SceneManagement.SceneManager.LoadScene("BackGarden");
        }

        seeker = FindAnyObjectByType<AILerp>();
        player = FindAnyObjectByType<PlayerController>();
        dialogue = FindAnyObjectByType<DialogueRunner>();
    }

    public void deathOccurred()
    {
        Debug.Log("DEATH");
        dialogue.StartDialogue("Death");
        dead = true;
    }
}