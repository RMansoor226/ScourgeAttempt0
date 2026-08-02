using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    Idle,
    Chasing, 
    Attacking, 
    Dead
}

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent _navMeshAgent;
    public ZombieState currentState;
    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip chaseClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip deathClip;

    [SerializeField] private float attackCooldown = 1.5f;
    private float _attackTimer;

    private ZombieSpawner _spawner;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        
        currentState = ZombieState.Idle;
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Idle:
                //Debug.Log("Zombie idle");
                _animator.SetBool("IsWalking", false);
                TryChasePlayer();
                break;
            case ZombieState.Chasing:
                Chase();
                TryAttackPlayer();
                break;
            case ZombieState.Attacking:
                DamagePlayer();
                break;
            case ZombieState.Dead:
                //Debug.Log("Zombie dead");
                break; // Death is handled by EnterDeadState()
            default:
                Debug.Log("Invalid Zombie AI State");
                break;
        }
    }

    public void Initialize(ZombieSpawner zombieSpawner)
    {
        _spawner = zombieSpawner;
    }

    private void ChangeState(ZombieState newState)
    {
        currentState = newState;

        if (newState == ZombieState.Chasing)
        {
            _navMeshAgent.isStopped = false;
        }

        if (newState == ZombieState.Attacking)
        {
            _navMeshAgent.isStopped = true;
        }
    }

    private void TryChasePlayer()
    {
        if (player != null)
        {
            ChangeState(ZombieState.Chasing);
            //Debug.Log("Zombie chasing");
        }
    }

    private void Chase()
    {
        if (player != null)
        {
            _navMeshAgent.SetDestination(player.position); 
            _animator.SetBool("IsWalking", true);
            
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = chaseClip;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }
    }

    private void TryAttackPlayer()
    {
        if (player == null)
        {
            return;
        }
        if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            ChangeState(ZombieState.Attacking);
            //Debug.Log("Zombie attacking");
        }
        else
        {
            ChangeState(ZombieState.Chasing);
        }
    }

    // Deals damage to player when the zombie's animation visually reaches them
    public void Attack()
    {
        float distance = Vector3.Distance(
            transform.position, 
            player.position);

        if (distance > _navMeshAgent.stoppingDistance)
        {
            //Debug.Log("Too far away to attack");
            return;
        }
        
        if (player.TryGetComponent(out IDamageable damageable))
        {
            //Debug.Log("Zombie damaging player");
            damageable.TakeDamage(25f);
        }
        else
        {
            //Debug.Log("Not Damageable");
        }
        
    }

    private void DamagePlayer()
    {
        if (player == null)
        {
            return;
        }
        
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );
        
        if (_attackTimer <= 0f)
        {
            if (distance <= _navMeshAgent.stoppingDistance)
            {
                _animator.SetTrigger("Attack");
                _attackTimer = attackCooldown;
            }
            else
            {
                //Debug.Log("Player escaped. Zombie chasing again");
                ChangeState(ZombieState.Chasing);
                _animator.SetBool("IsWalking", true);
                return;
            }
        }
        _attackTimer -= Time.deltaTime;
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

    // A public method to allow other scripts like Health to inform Zombie AI of death
    public void EnterDeadState()
    {
        if (currentState == ZombieState.Dead)
        {
            return;
        }
        
        ChangeState(ZombieState.Dead);
        
        _navMeshAgent.enabled = false;
        _animator.SetTrigger("Death");
        PlayDeathSound();

        Destroy(gameObject, 5f);
    }
}
