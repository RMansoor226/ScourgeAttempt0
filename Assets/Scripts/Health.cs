using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Health remaining: {_currentHealth}");
        
        if (_currentHealth <= 0f)
        {
            Destroy(gameObject); // Object dies
        }
    }
}

public interface IDamageable
{
    public void TakeDamage(float damageAmount);
}
