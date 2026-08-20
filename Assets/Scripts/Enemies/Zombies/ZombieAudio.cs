using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    private ZombieAI _zombieAI;

    [SerializeField] 
    private float minGrowlInterval = 5f;
    [SerializeField] 
    private float maxGrowlInterval = 7f;
    
    private float _nextGrowlTime;
    
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip[] chaseClips;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip deathClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _zombieAI = GetComponent<ZombieAI>();
    }

    private void Update()
    {
        if (_zombieAI.currentState != ZombieState.Chasing)
        {
            return;
        }

        if (_audioSource.isPlaying)
        {
            return;
        }
        
        if (Time.time >= _nextGrowlTime)
        {
            PlayGrowl();

            _nextGrowlTime = Time.time +
                            Random.Range(minGrowlInterval, maxGrowlInterval);
        }
    }

    private void PlayGrowl()
    {
        int randomIndex = Random.Range(0, chaseClips.Length);
        
        _audioSource.clip = chaseClips[randomIndex];
        _audioSource.loop = false;
        _audioSource.Play();
    }
    
    // Plays the attack sound when at the beginning of the zombie attack animation clip
    public void PlayAttackSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.PlayOneShot(attackClip);
    }

    public void PlayDeathSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        
        _audioSource.PlayOneShot(deathClip);
    }
}
