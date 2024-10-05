using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float detectionRange;
    [SerializeField] float attackDelay;
    [SerializeField] float attackDamage;

    Rigidbody rb;
    NavMeshAgent agent;
    Animator animator;
    Transform player;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if(!playerInRange())
        {
            Movement();
        }
        else
        {
            Attack();
        }
    }
    protected virtual void Movement()
    {
        
    }

    protected virtual void Attack()
    {
        
    }
    bool playerInRange()
    {
        if(Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            return true;
        }
        return false;
    }
}
