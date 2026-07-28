using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab;

    
    public Transform[] spawnPoints;

   
    public int startingEnemies = 3; // first wave

    
    public float timeBetweenWaves = 10f;

    private int currentWave = 1;
    private int enemiesToSpawn;

    void Start()
    {
        enemiesToSpawn = startingEnemies;

        
        Invoke(nameof(SpawnWave), 2f);
    }

    void SpawnWave()
    {
          for (int i = 0; i < enemiesToSpawn; i++)
    {
        SpawnEnemy();
    }
        {
            SpawnEnemy();
        }

        
        enemiesToSpawn++;

        currentWave++;

       
        Invoke(nameof(SpawnWave), timeBetweenWaves);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab has not been assigned.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points have been assigned.");
            return;
        }

        // Pick one of the spawn points randomly
        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        Instantiate(
            enemyPrefab,
            chosenSpawnPoint.position,
            chosenSpawnPoint.rotation
        );
    }
}