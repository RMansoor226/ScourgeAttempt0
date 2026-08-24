using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Health _health;
    private ZombieAI _ai;
    private ZombieAudio _audio;
    private ZombiePool _zombiePool;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _ai = GetComponent<ZombieAI>();
        _audio = GetComponent<ZombieAudio>();

        _health.OnDeath += HandleDeath;
        _ai.OnZombieStateChanged += HandleStateChanged;
    }

    public void Initialize(WaveSettings settings)
    {
        _health.Initialize(settings.zombieHealth);
        _ai.Initialize(settings.zombieSpeed);
        _audio.Initialize();
    }

    private void HandleDeath()
    {
        _ai.EnterDeadState();
        _audio.PlayDeathSound();
        StartCoroutine(ReturnToPoolAfterDelay(5f));
    }

    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _zombiePool.ReturnZombie(this);
    }

    private void HandleStateChanged(ZombieState state)
    {
        switch (state)
        {
            case ZombieState.Chasing:
                _audio.StartChasingAudio();
                break;
            case ZombieState.Attacking:
            case ZombieState.Dead:
            case ZombieState.Idle:
                _audio.StopChasingAudio();
                break;
        }
    }

    public void Attack()
    {
        _ai.Attack();
    }

    public void PlayAttackSound()
    {
        _audio.PlayAttackSound();
    }

    public void Reset()
    {
        _health.Reset();
        _ai.Reset();
        _audio.Reset();
    }

    public Health GetZombieHealth()
    {
        return _health;
    }

    public void SetZombiePool(ZombiePool pool)
    {
        _zombiePool = pool;
    }
}
