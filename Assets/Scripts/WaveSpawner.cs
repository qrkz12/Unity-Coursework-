using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int startingEnemies = 3;
    public int extraEnemiesPerWave = 1;
    public float timeBetweenWaves = 8f;

    [Header("Level Settings")]
    public int[] wavesPerLevel = { 3, 5 };

    [Header("UI")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI waveText;

    private int currentLevelIndex;
    private int currentWaveInLevel;
    private int totalWavesCompleted;

    private void Start()
    {
        currentLevelIndex = 0;
        currentWaveInLevel = 0;
        totalWavesCompleted = 0;

        UpdateUI();

        StartCoroutine(LevelAndWaveLoop());
    }

    private IEnumerator LevelAndWaveLoop()
    {
        // Brief delay before the first wave.
        yield return new WaitForSeconds(2f);

        while (currentLevelIndex < wavesPerLevel.Length)
        {
            int wavesInCurrentLevel =
                wavesPerLevel[currentLevelIndex];

            while (currentWaveInLevel < wavesInCurrentLevel)
            {
                currentWaveInLevel++;

                UpdateUI();

                int enemiesThisWave =
                    startingEnemies +
                    totalWavesCompleted * extraEnemiesPerWave;

                Debug.Log(
                    $"Level {currentLevelIndex + 1}, " +
                    $"Wave {currentWaveInLevel}/{wavesInCurrentLevel}: " +
                    $"spawning {enemiesThisWave} enemies"
                );

                SpawnWave(enemiesThisWave);

                totalWavesCompleted++;

                yield return new WaitForSeconds(timeBetweenWaves);
            }

            // Current level has been completed.
            Debug.Log($"Level {currentLevelIndex + 1} complete");

            currentLevelIndex++;
            currentWaveInLevel = 0;

            if (currentLevelIndex < wavesPerLevel.Length)
            {
                UpdateUI();

                // Longer break between levels.
                yield return new WaitForSeconds(5f);
            }
        }

        levelText.text = "All Levels Complete";
        waveText.text = "";

        Debug.Log("All levels complete");
    }

    private void SpawnWave(int enemyCount)
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

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex =
            Random.Range(0, spawnPoints.Length);

        Transform selectedSpawnPoint =
            spawnPoints[randomIndex];

        Instantiate(
            enemyPrefab,
            selectedSpawnPoint.position,
            selectedSpawnPoint.rotation
        );
    }

    private void UpdateUI()
    {
        if (currentLevelIndex >= wavesPerLevel.Length)
        {
            return;
        }

        int displayedLevel = currentLevelIndex + 1;
        int displayedWave = currentWaveInLevel;
        int wavesInLevel = wavesPerLevel[currentLevelIndex];

        if (levelText != null)
        {
            levelText.text = $"Level {displayedLevel}";
        }

        if (waveText != null)
        {
            waveText.text =
                $"Wave {displayedWave} / {wavesInLevel}";
        }
    }
}