using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using OurGame.Utils;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistMax = 7f;
    [SerializeField] private float roamingDistMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float pathCheckInterval = 0.5f;
    [SerializeField] private float chasingDistance = 5f;
    [SerializeField] private float desiredDistance = 4f;

    [SerializeField] private Transform target;
    [SerializeField] Rigidbody2D rb;
    public float speed;
    private bool facingRight = false;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;
    private Coroutine pathCheckCoroutine;

    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death,
        Panicking
    }

    private void Start()
    {
        startingPosition = transform.position;
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    private void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        switch (state)
        {
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                CheckForPlayer();
                
                break;
            case State.Chasing:
                ChasePlayer();
                CheckForPlayer();
                break;
            case State.Attacking:
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
            case State.Panicking:
                MoveAwayFromPlayer();
            break;
        }

        float dirX = navMeshAgent.velocity.normalized.x;
        float dirY = navMeshAgent.velocity.normalized.y;

        if (dirX < 0 && facingRight)
        {
            Flip();
        }
        else if (dirX > 0 && !facingRight)
        {
            Flip();
        }

    }

    void FixedUpdate()
    {
        var dir = (target.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }
    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, target.position) <= chasingDistance)
        {
            if (gameObject.tag == "EnemyShoot")
            {
                state = State.Panicking;
            }
            else
            {
                state = State.Chasing;
            }
        }
        else
        {
            state = State.Roaming;
        }
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 directionToPlayer = transform.position - target.position;
        directionToPlayer.Normalize();

        Vector3 targetPosition = transform.position + directionToPlayer * desiredDistance;

        navMeshAgent.SetDestination(targetPosition);
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(target.position);
    }

    public bool IsRunning()
    {
        return navMeshAgent.velocity != Vector3.zero;
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
        if (pathCheckCoroutine != null)
        {
            StopCoroutine(pathCheckCoroutine);
        }
        pathCheckCoroutine = StartCoroutine(CheckPath());
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 randomDirection = Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistMin, roamingDistMax);
        return startingPosition + randomDirection;
    }

    private IEnumerator CheckPath()
    {
        while (true)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                roamPosition = GetRoamingPosition();
                navMeshAgent.SetDestination(roamPosition);
            }
            yield return new WaitForSeconds(pathCheckInterval);
        }
    }
}