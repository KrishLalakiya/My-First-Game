using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("The enemy prefab to be spawned.")]
    public GameObject enemyPrefab;

    [Tooltip("An array of points where enemies can be spawned.")]
    public Transform[] spawnPoints;

    [Tooltip("The time in seconds between each spawn.")]
    public float spawnInterval = 5f;

    [Tooltip("The maximum number of enemies allowed in the scene at once.")]
    public int maxEnemies = 10;

    // A private timer to track time until the next spawn.
    private float timer;

    void Update()
    {
        // First, count how many enemies are currently in the scene.
        // We use FindGameObjectsWithTag, so your enemy prefab MUST have the "Enemy" tag.
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // If the number of enemies is less than the maximum...
        if (currentEnemyCount < maxEnemies)
        {
            // ...add time to our timer.
            timer += Time.deltaTime;

            // If the timer has reached the spawn interval...
            if (timer >= spawnInterval)
            {
                // ...reset the timer and spawn an enemy.
                timer = 0f;
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        // If we don't have any spawn points, exit out to avoid errors.
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the EnemySpawner.");
            return;
        }

        // Pick a random index from our array of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[spawnPointIndex];

        // Create a new enemy at the chosen spawn point's position and rotation.
        Instantiate(enemyPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);
        
        Debug.Log("An enemy has spawned at " + chosenSpawnPoint.name);
    }
}