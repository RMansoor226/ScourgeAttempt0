using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] 
    private float minGrowlInterval = 5f;
    [SerializeField] 
    private float maxGrowlInterval = 7f;
    
    private float _nextGrowlTime;
    private bool _isChasing;
    
    [SerializeField] 
    private AudioClip idleClip;
    [SerializeField] 
    private AudioClip[] chaseClips;
    [SerializeField] 
    private AudioClip attackClip;
    [SerializeField] 
    private AudioClip deathClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_isChasing)
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

    public void Initialize()
    {
        _isChasing = false;
        _audioSource.clip = idleClip;
        _nextGrowlTime = 0f;
    }

    public void StartChasingAudio()
    {
        _isChasing = true;
    }
    
    public void StopChasingAudio()
    {
        _isChasing = false;
    }

    private void PlayGrowl()
    {
        if (chaseClips.Length == 0)
        {
            return;
        }
        
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

    public void Reset()
    {
        _isChasing = false;
        _audioSource.Stop();
        _nextGrowlTime = 0f;
    }
}
