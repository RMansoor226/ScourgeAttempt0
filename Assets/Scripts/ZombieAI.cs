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

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = ZombieState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Idle:
                Debug.Log("Zombie idle");
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
    }

    private void Attack()
    {
        
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

        if (distance > _navMeshAgent.stoppingDistance)
        {
            ChangeState(ZombieState.Chasing);
            Debug.Log("Zombie chasing again");
            return;
        }
        
        Debug.Log("Zombie damaging player");
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
        
        Destroy(gameObject, 5f);
    }
}
