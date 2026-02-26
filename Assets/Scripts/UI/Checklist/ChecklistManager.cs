using UnityEngine;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance;

    public int phase = 1;
    public bool personKilled = false;

    public bool evidenceRewritten = false;
    public bool bloodCleaned = false;
    public bool bodyMoved = false;

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

}