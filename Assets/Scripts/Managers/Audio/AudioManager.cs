using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance { get; set; }

    [SerializeField] 
    private AudioSource baseMusicSource;
    [SerializeField] 
    private AudioSource roundMusicSource;
    [SerializeField] 
    private AudioSource sfxSource;
    [SerializeField] 
    private AudioSource uiSource;
    [SerializeField] 
    private AudioSource ambientSource;

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

        baseMusicSource.volume = 1f;
        roundMusicSource.volume = 0.125f;
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        
        sfxSource.PlayOneShot(clip);
    }
    
    public void PlayBaseMusic(AudioClip clip, bool shouldLoop)
    {
        if (clip == null)
        {
            return;
        }

        baseMusicSource.clip = clip;
        baseMusicSource.loop = shouldLoop;
        baseMusicSource.Play();
    }
    
    public void PlayRoundMusic(AudioClip clip, bool shouldLoop)
    {
        if (clip == null)
        {
            return;
        }
        
        roundMusicSource.PlayOneShot(clip);
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
        baseMusicSource.Stop();
    }
}
