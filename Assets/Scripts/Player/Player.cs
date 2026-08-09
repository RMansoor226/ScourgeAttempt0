using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health { get; set; }
    public PlayerInputHandler Inputs { get; set; }
    public PlayerLook Look { get; set; }
    public PlayerMovement Movement { get; set; }
    public PlayerWeaponController Weapons { get; set; }

    [SerializeField] private float playerHealth = 100f;

    private bool _isDead;
    
    private void Awake()
    {
        Health = GetComponent<Health>();
        if (Health == null)
        {
            Debug.LogError("Health is not instantiated");
        }
        
        Inputs = GetComponent<PlayerInputHandler>();
        if (Inputs == null)
        {
            Debug.LogError("Inputs is not instantiated");
        }
        
        Look = GetComponentInChildren<PlayerLook>();
        if (Look == null)
        {
            Debug.LogError("Look is not instantiated");
        }
        
        Movement = GetComponentInChildren<PlayerMovement>();
        if (Movement == null)
        {
            Debug.LogError("Movement is not instantiated");
        }
        
        Weapons = GetComponent<PlayerWeaponController>();
        if (Weapons == null)
        {
            Debug.LogError("Weapons is not instantiated");
        }
    }

    private void OnEnable()
    {
        if (Health != null)
        {
            Health.OnDeath += PlayerDied;
        }
    }

    private void OnDisable()
    {
        if (Health != null)
        {
            Health.OnDeath -= PlayerDied;
        }
    }

    public void Initialize()
    {
        if (Health == null)
        {
            Debug.LogError("Player.Initialize failed: Health component is missing!");
        }
        Health.Initialize(playerHealth); 
        _isDead = false;
    }

    private void PlayerDied()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;

        Debug.Log("Player has died!");
    }
}
