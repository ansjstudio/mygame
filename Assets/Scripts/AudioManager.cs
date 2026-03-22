using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Audio Clips")]
    public AudioClip backGroundMusic;
    public AudioClip buttonClickSFX;

    private AudioManager instance;

    public AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<AudioManager>();
                if (instance == null)
                {
                    Debug.LogError("AudioManager not found");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("AudioManager instance is null in Awake");
        }
    }

    private void Start()
    {
        PlayMusic(backGroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicAudioSource.clip == clip) return;
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}
