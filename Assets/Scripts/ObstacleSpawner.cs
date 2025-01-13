using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // Array of different obstacle prefabs to spawn
    public Transform player;              // The player transform
    public float spawnDistance = 10f;     // Distance from the player to spawn the obstacles
    public float spawnInterval = 2f;      // Time interval between each spawn
    public float minHeight = -4.5f;      // Minimum height for spawning
    public float maxHeight = 10f;        // Maximum height for spawning

    private Coroutine spawnCoroutine;    // Reference to the spawn coroutine

    private void Start()
    {
        // Start the spawning coroutine
        spawnCoroutine = StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Check if the game is started
            if (GameManagement.Instance != null && GameManagement.Instance.IsGameStarted())
            {
                SpawnObstacle(); // Spawn an obstacle
            }

            yield return new WaitForSeconds(spawnInterval); // Wait for the interval
        }
    }

    private void SpawnObstacle()
    {
        // Ensure there are obstacles to spawn
        if (obstaclePrefabs.Length == 0)
        {
            Debug.LogError("No obstacle prefabs assigned!");
            return;
        }

        // Choose a random prefab from the available ones
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedPrefab = obstaclePrefabs[randomIndex];

        // Calculate spawn position in front of the player
        Vector3 spawnPosition = player.position + player.forward * spawnDistance;
        spawnPosition.y = Random.Range(minHeight, maxHeight);  // Set random height within the range

        // Instantiate the selected obstacle prefab at the calculated position
        GameObject obstacle = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // Get the ObstacleMovement script and set the player's Z position for comparison
        ObstacleMovement obstacleMovement = obstacle.GetComponent<ObstacleMovement>();
        if (obstacleMovement != null)
        {
            obstacleMovement.SetPlayerZPosition(player.position.z);  // Pass the player's Z position to the obstacle
        }
    }

    private void OnDestroy()
    {
        // Stop the coroutine when the spawner is destroyed
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }
}
