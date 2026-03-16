using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public int phase = 1;
    public Vector3 storedPos;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        Instance = this;
    }
}