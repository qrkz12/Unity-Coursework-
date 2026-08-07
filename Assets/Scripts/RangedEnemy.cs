using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float preferredDistance = 7f;
    public float distanceTolerance = 1f;
    public float retreatDistance = 3f;

    private Transform player;
    private NavMeshAgent agent;
    private EnemyShoot enemyShoot;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyShoot = GetComponent<EnemyShoot>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh)
        {
            return;
        }

        float distanceToPlayer =
            Vector3.Distance(
                transform.position,
                player.position
            );

        HandleMovement(distanceToPlayer);
        FacePlayer();

        if (
            enemyShoot != null &&
            distanceToPlayer <= preferredDistance + distanceTolerance
        )
        {
            enemyShoot.TryShoot(player);
        }
    }

    void HandleMovement(float distanceToPlayer)
    {
        float minimumDistance =
            preferredDistance - distanceTolerance;

        float maximumDistance =
            preferredDistance + distanceTolerance;

        if (distanceToPlayer > maximumDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else if (distanceToPlayer < minimumDistance)
        {
            Vector3 awayDirection =
                (transform.position - player.position).normalized;

            Vector3 retreatPosition =
                transform.position +
                awayDirection * retreatDistance;

            if (
                NavMesh.SamplePosition(
                    retreatPosition,
                    out NavMeshHit hit,
                    retreatDistance,
                    NavMesh.AllAreas
                )
            )
            {
                agent.isStopped = false;
                agent.SetDestination(hit.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
        else
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }

    void FacePlayer()
    {
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation =
                Quaternion.LookRotation(direction);
        }
    }
}