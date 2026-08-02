using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;
    private bool _isDead;
    [SerializeField] private bool invincible = false;
    
    private void Awake()
    {
        _currentHealth = maxHealth;
        _isDead = false;
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Health remaining: {_currentHealth}");
        
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
            Debug.Log($"{gameObject.name} is invincible.");
            _currentHealth = 1f;   // Prevent repeatedly calling Die()
            return;
        }
        
        ZombieAI zombieAI = GetComponent<ZombieAI>();
            
        if (zombieAI != null)
        {
            zombieAI.EnterDeadState();
            return;
        }
        
        Destroy(gameObject); // Object dies
    }
}


public interface IDamageable
{
    public void TakeDamage(float damageAmount);
}
