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

    [SerializeField] private float attackCooldown = 1.5f;
    private float _attackTimer;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = ZombieState.Idle;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Idle:
                Debug.Log("Zombie idle");
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
                Debug.Log("Zombie dead");
                break; // Death is handled by EnterDeadState()
            default:
                Debug.Log("Invalid Zombie AI State");
                break;
        }
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
            Debug.Log("Zombie chasing");
        }
    }

    private void Chase()
    {
        if (player != null)
        {
            _navMeshAgent.SetDestination(player.position); 
            _animator.SetBool("IsWalking", true);
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
            Debug.Log("Zombie attacking");
        }
        else
        {
            ChangeState(ZombieState.Chasing);
        }
    }

    public void Attack()
    {
        if (player.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("Zombie damaging player");
            damageable.TakeDamage(25f);
        }
        else
        {
            Debug.Log("Not Damageable");
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
                Debug.Log("Player escaped. Zombie chasing again");
                ChangeState(ZombieState.Chasing);
                _animator.SetBool("IsWalking", true);
                return;
            }
        }
        _attackTimer -= Time.deltaTime;
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
        
        Destroy(gameObject, 5f);
    }
}
