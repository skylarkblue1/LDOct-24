using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMask GroundLayer, PlayerLayer, ProjectileLayer;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;

    [Header("AI Configuration")]
    [SerializeField]
    private bool disableAI;
    [SerializeField]
    private float walkPointRange;
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float sightRange, attackRange;

    [Header("Stats Configuration")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int attackDamage;

    [Header("Stats Configuration")]
    [SerializeField]
    private StoryTriggerZone story;

    private Transform playerTransform;
    private HealthSystem healthSystem;
    private PlayerAttack playerAttack;

    private Vector3 lookPos;

    // This is here as a quick fix to make sure a projectile hits once.
    private float cooldown = .5f;
    private bool isOnCooldown = false;

    // If the enemy gets hit, force it to look for the player
    private bool aggroPlayer = false;
    private float aggroReset = 5f;
    private float aggroCount = 0;

    private bool willSelfDestruct = false;

    private bool playerInSightRange, playerInAttackRange, projectileInHitRange;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = Player.transform;
        healthSystem = Player.GetComponent<HealthSystem>();
        playerAttack = Player.GetComponent<PlayerAttack>();
    }

    void Update()
    {
        lookPos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(lookPos);

        if (disableAI)
            return;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);
        projectileInHitRange = Physics.CheckSphere(transform.position, attackRange, ProjectileLayer);

        // if(projectileInHitRange) TookDamage();

        // anti-error conditional
        if (willSelfDestruct)
            return;

        if (aggroPlayer)
        {
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
            else ChasePlayer();

            aggroCount += Time.deltaTime;
            if(aggroCount >= aggroReset)
            {
                aggroPlayer = false;
                aggroCount = 0;
            }
            return;
        }
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    public void TookDamage()
    {
        if (isOnCooldown) return;

        health -= playerAttack.attackDamage;
        Debug.Log("hit! "+health);
        aggroCount = 0;
        isOnCooldown = true;
        aggroPlayer = true;

        CheckIsDead();
        Invoke(nameof(DisableCooldown), cooldown);
    }

    void DisableCooldown()
    {
        isOnCooldown = false;
    }

    void CheckIsDead()
    {
        if (health > 0) return;
        if (story != null) story.TriggerStory();
        Debug.Log("ded");
        this.willSelfDestruct = true;
        Destroy(this.gameObject);
    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            return;
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, GroundLayer))
            walkPointSet = true;
    }
    void ChasePlayer()
    {
        agent.SetDestination(lookPos);
    }

    void AttackPlayer()
    {
        agent.SetDestination(lookPos);

        if (!alreadyAttacked)
        {
            //Attack here
            healthSystem.DecreaseHealth(attackDamage);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
