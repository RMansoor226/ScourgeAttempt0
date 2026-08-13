using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] 
    private AudioSource musicSource;
    [SerializeField] 
    private AudioSource sfxSource;

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
    
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
