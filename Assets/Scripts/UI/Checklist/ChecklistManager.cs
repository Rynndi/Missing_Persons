using UnityEngine;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance;

    public int collectibleCount = 0;

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