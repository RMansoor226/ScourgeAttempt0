using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance { get; set; }

    [SerializeField] 
    private AudioSource musicSource;
    [SerializeField] 
    private AudioSource sfxSource;
    [SerializeField] 
    private AudioSource uiSource;

    private void Awake()
    {
        // Destroy any pre-existing audio managers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        
        sfxSource.PlayOneShot(clip);
    }
    
    public void PlayMusic(AudioClip clip, bool shouldLoop)
    {
        if (clip == null)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = shouldLoop;
        musicSource.Play();
    }
    
    public void PlayUiSfx(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        uiSource.clip = clip;
        uiSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
