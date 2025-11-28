using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // --- SETTINGS ---
    [Header("Player")]
    public Transform player;
    public string playerTag = "Player";

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 3f;
    public float patrolWaitTime = 1f;

    [Header("Detection")]
    public float sightRange = 12f;
    public float viewAngle = 120f;
    public float attackRange = 2f;

    [Header("Combat")]
    public int damage = 10;
    public float attackCooldown = 1f;
    public float damageDelay = 0.2f;

    [Header("Health")]
    public int maxHealth = 100;

    // --- INTERNALS ---
    private int currentHealth;
    private float lastAttackTime;
    private NavMeshAgent agent;
    private int patrolIndex;
    private Animator anim;
    private bool isAttacking = false;

    private enum State { Patrol, Chase, Attack }
    private State state = State.Patrol;


    private void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
        }

        GoToNextPatrolPoint();
    }


    private void Update()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);

        bool canSeePlayer = CanSeePlayer();

        // --- STATE MACHINE ---
        switch (state)
        {
            case State.Patrol:
                Patrol();

                if (canSeePlayer)
                    state = State.Chase;
                break;

            case State.Chase:
                Chase();

                if (dist <= attackRange)
                    state = State.Attack;
                if (!canSeePlayer)
                    ReturnToPatrol();
                break;

            case State.Attack:
                Attack();

                if (dist > attackRange + 0.3f)
                {
                    agent.isStopped = false;
                    state = State.Chase;
                }
                break;
        }

        UpdateAnimator();
    }

    // -------------------------
    // PATROL
    // -------------------------
    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[patrolIndex].position;
    }

    // -------------------------
    // CHASE
    // -------------------------
    private void Chase()
    {
        agent.isStopped = false;
        agent.speed = patrolSpeed * 2f;
        agent.SetDestination(player.position);
    }

    private void ReturnToPatrol()
    {
        state = State.Patrol;
        GoToNextPatrolPoint();
    }

    // -------------------------
    // ATTACK
    // -------------------------
    private void Attack()
    {
        agent.isStopped = true;

        // rotate toward player
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            isAttacking = true;
            if (anim) anim.SetTrigger("attack");
            Invoke(nameof(DealDamage), damageDelay);
        }
    }

    private void DealDamage()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange + 0.5f)
        {
            Health p = player.GetComponent<Health>();
            if (p != null) p.TakeDamage(damage);
        }
        isAttacking = false;
    }

    // -------------------------
    // DETECTION
    // -------------------------
    private bool CanSeePlayer()
    {
        Vector3 dir = (player.position - transform.position);
        float dist = dir.magnitude;

        if (dist > sightRange) return false;

        float angle = Vector3.Angle(transform.forward, dir);
        if (angle > viewAngle * 0.5f) return false;

        return true;
    }

    // -------------------------
    // ANIMATOR
    // -------------------------
    private void UpdateAnimator()
    {
        if (!anim) return;

        anim.SetBool("isMoving", agent.velocity.magnitude > 0.1f);
    }

    // -------------------------
    // HEALTH
    // -------------------------
    public void TakeDamage(int amt)
    {
        currentHealth -= amt;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}


//--------------------------------------------------------------
// SIMPLE Player/Enemy HEALTH CLASS (embedded)
//--------------------------------------------------------------
public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amt)
    {
        currentHealth -= amt;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}