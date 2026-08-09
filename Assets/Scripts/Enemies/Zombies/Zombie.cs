using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Health _health;
    private ZombieAI _ai;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _ai = GetComponent<ZombieAI>();
    }

    public void Initialize(WaveSettings settings)
    {
        _health.Initialize(settings.zombieHealth);
        _ai.Initialize(settings.zombieSpeed);
    }

    public Health GetZombieHealth()
    {
        return _health;
    }
}
