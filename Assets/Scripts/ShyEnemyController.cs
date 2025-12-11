using UnityEngine;

public class ShyEnemyController : MonoBehaviour
{
    [Header("Core Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float detectionRadius = 8.0f;
    [SerializeField] private float memoryTime = 2f; // <-- ¡NUEVA VARIABLE! El tímido tiene menos memoria.

    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float patrolWaitTime = 3f;

    // --- NUEVAS VARIABLES DE ESTADO Y TEMPORIZADOR ---
    private bool isFleeing = false;
    private float memoryTimer;

    private Vector3 startPosition;
    private Vector3 patrolDestination;
    private float waitTimer;

    void Start()
    {
        startPosition = transform.position;
        SetNewPatrolDestination();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // --- LÓGICA DE DETECCIÓN Y MEMORIA ---
        if (distanceToPlayer < detectionRadius)
        {
            isFleeing = true;
            memoryTimer = memoryTime;
        }
        else
        {
            if (isFleeing)
            {
                memoryTimer -= Time.deltaTime;
                if (memoryTimer <= 0)
                {
                    isFleeing = false;
                }
            }
        }

        // --- LÓGICA DE ACCIÓN BASADA EN EL ESTADO ---
        if (isFleeing)
        {
            Flee();
        }
        else
        {
            Patrol();
        }
    }

    private void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        fleeDirection.y = 0;
        transform.Translate(fleeDirection * moveSpeed * Time.deltaTime);
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolDestination) < 1f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= patrolWaitTime)
            {
                SetNewPatrolDestination();
                waitTimer = 0f;
            }
        }
        else
        {
            Vector3 direction = (patrolDestination - transform.position).normalized;
            direction.y = 0;
            transform.Translate(direction * (moveSpeed * 0.5f) * Time.deltaTime);
        }
    }

    private void SetNewPatrolDestination()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        patrolDestination = new Vector3(startPosition.x + randomX, transform.position.y, startPosition.z + randomZ);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}