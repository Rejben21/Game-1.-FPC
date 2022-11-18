using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator animator;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    private float walkPointRange = 5f;

    private float timeBetweenAttacks = 0.5f;
    bool alreadyAttacked;

    private float attackRange;
    private float sightRange = 100;
    private bool playerInSightRange, playerInAttackRange;
    private bool isChaseing, isAttacking;

    private bool isMage, isArcher;
    public GameObject arrowPrefab, ballPrefab;

    public EnemyType type;

    public enum EnemyType
    {
        Skeleton,
        Warrior,
        Archer,
        Mage
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        switch(type)
        {
            case EnemyType.Skeleton:
                attackRange = 1;
                break;

            case EnemyType.Warrior:
                attackRange = 1.2f;
                break;

            case EnemyType.Archer:
                attackRange = 15;
                isArcher = true;
                break;

            case EnemyType.Mage:
                attackRange = 10;
                isMage = true;
                break;
        }
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            isChaseing = true;
        }
        else
        {
            isChaseing = false;
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        animator.SetBool("IsFollowing", isChaseing);
        animator.SetBool("IsAttacking", isAttacking);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void ArcherAttack()
    {
        if(isArcher)
        {
            Instantiate(arrowPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        }
        else
        {
            return;
        }
    }
    private void MageAttack()
    {
        if (isMage)
        {
            Instantiate(ballPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
        }
        else
        {
            return;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        Vector3 lookPos = player.position - transform.position;
        if(!isMage && !isArcher)
        {
            lookPos.y = 0;
        }
        else
        {
            lookPos.y = lookPos.y / 2;
        }
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
