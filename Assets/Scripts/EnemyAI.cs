using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No GameObject with the Player tag was found.");
        }
    }

    void Update()
    {
        if (player != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }
}