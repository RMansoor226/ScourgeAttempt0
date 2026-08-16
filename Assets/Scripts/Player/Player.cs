using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    private Health _health;
    private PlayerInputHandler _inputs;
    private PlayerLook _look;
    private PlayerMovement _movement;
    private PlayerWeaponController _weapons;
    private PlayerDamageFlinch _flinch;

    private AudioSource _playerAudio;
    [SerializeField] 
    private AudioClip painClip;
    [SerializeField] 
    private AudioClip deathClip;
    [SerializeField] 
    private AudioClip[] footsteps;

    [SerializeField]
    private HealthBar healthBar;
    [SerializeField] 
    private DamageVignette vignette;
    [SerializeField] 
    private DamageFlash flash;

    private float _maxHealth = 100f;
    private float _playerHealth;

    private bool _isDead;
    private bool _footstepsPlaying;

    private void Awake()
    {
        CheckAndInstantiate(ref _health, "Health");
        CheckAndInstantiate(ref _inputs, "Inputs");
        CheckAndInstantiate(ref _look, "Look");
        CheckAndInstantiate(ref _movement, "Movement");
        CheckAndInstantiate(ref _weapons, "Weapons");
        CheckAndInstantiate(ref _flinch, "Flinch");
        
        CheckAndInstantiate(ref _playerAudio, "Audio");

        _playerHealth = _maxHealth;
        _footstepsPlaying = false;
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDeath += PlayerDied;
            _health.OnHealthChanged += PlayerHurt;
        }

        if (_movement != null)
        {
            _movement.OnMovement += PlayerMoved;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDeath -= PlayerDied;
            _health.OnHealthChanged -= PlayerHurt;
        }
        
        if (_movement != null)
        {
            _movement.OnMovement -= PlayerMoved;
        }
    }

    public void Initialize()
    {
        if (_health == null)
        {
            Debug.LogError("Player.Initialize failed: Health component is missing!");
        }

        _health.Initialize(_playerHealth);
        
        _isDead = false;
    }

    private void PlayerHurt(float _currentHealth, float _newMaxHealth)
    {
        CheckAndPlayClip(painClip, "Pain");
        
        _playerHealth = _currentHealth;
        float percentHealth = (_playerHealth / _newMaxHealth);

        _flinch.FlinchPlayer();
        
        healthBar.UpdateHealthBar(percentHealth);
        vignette.UpdateVignetteIntensity(percentHealth);
        StartCoroutine(flash.UpdateDamageFlash());
    }
    
    private void PlayerDied()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;

        CheckAndPlayClip(deathClip, "Death");
        
        //Debug.Log("Player has died!");

        gameManager.PlayerDied(
            _inputs, 
            _look, 
            _movement, 
            _weapons, 
            _flinch
        );
    }

    private void PlayerMoved()
    {
        //Debug.Log("Player footsteps should be playing!");

        if (!_footstepsPlaying)
        {
            StartCoroutine(PlayFootstepAudio());
        }
    }
    
    private IEnumerator PlayFootstepAudio()
    {
        CheckAndPlayClip(footsteps[Random.Range(0, footsteps.Length)], "Footsteps");
        _footstepsPlaying = true;

        yield return new WaitForSeconds(0.5f);

        _footstepsPlaying = false;
    }

    private T CheckAndInstantiate<T>(ref T component, string componentName) where T : Component
    {
        if (component == null)
        {
            component = GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"{componentName} is not instantiated");
            }
        }
    
        return component;
    }

    private void CheckAndPlayClip(AudioClip audioClip, string clipName)
    {
        if (audioClip != null)
        {
            _playerAudio.clip = audioClip;
            _playerAudio.Play();
        }
        else
        {
            Debug.Log($"{clipName} wasn't attached to Player!");
        }
    }
}
