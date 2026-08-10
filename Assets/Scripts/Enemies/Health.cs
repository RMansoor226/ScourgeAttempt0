using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float _maxHealth;
    private float _currentHealth;
    private bool _isDead;
    
    [SerializeField] private bool invincible;

    public Action<float, float> OnHealthChanged;
    public Action OnDeath;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    public void Initialize(float initialHealth)
    {
        _maxHealth = initialHealth;
        _currentHealth = initialHealth;
    }
    
    public void TakeDamage(float damageAmount)
    {
        //Debug.Log($"{gameObject.name} had {_currentHealth} health.");
        _currentHealth -= damageAmount;
        
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Health remaining: {_currentHealth}");
        
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead)
        {
            return;
        }
        
        _isDead = true;
        
        if (invincible)
        {
            //Debug.Log($"{gameObject.name} is invincible.");
            _currentHealth = 1f;   // Prevent repeatedly calling Die()
            return;
        }

        OnDeath?.Invoke();
        
        ZombieAI zombieAI = GetComponent<ZombieAI>();
            
        if (zombieAI != null)
        {
            zombieAI.EnterDeadState();
            return;
        }
        
        //Destroy(gameObject); // Object dies
    }
}

public interface IDamageable
{
    public void TakeDamage(float damageAmount);
}
