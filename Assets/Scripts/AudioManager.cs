using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField]
    public AudioMixer mixer;

    public const string MasterVolumeKey = "MasterVolume";

    public float currentVol;
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

    void Start()
    {
        currentVol = PlayerPrefs.GetFloat(MasterVolumeKey, 0.75f);
        SetMasterVolume(currentVol);
     }

    public void SetMasterVolume(float percentage)
    {
        mixer.SetFloat("MasterVolume", ConvertToDecibel(percentage));
        currentVol = percentage;
        PlayerPrefs.SetFloat(MasterVolumeKey, currentVol);
        PlayerPrefs.Save();
    }

    private float ConvertToDecibel(float linearVolume)
    {
        if (linearVolume != 0)
        {
            return 20f * Mathf.Log10(linearVolume);
        }
        else
        {
            return -80f;
        }
    }
     
}