using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // To load the scene

public class EndGameWhenCollision : MonoBehaviour
{
    public ScoreCounter playerScore; // Reference to PlayerScore to call OnGameOver()
    public AudioSource gameOverSound; // Reference to the AudioSource for the game-over sound
    public float delayBeforeRestart = 2f; // Delay in seconds before restarting the scene

    private bool isGameOver = false; // Prevent multiple triggers

    // When the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Log the name of the object the player collided with
        Debug.Log("Triggered with: " + other.gameObject.name);

        // Only trigger game over logic if the player collided with an obstacle
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with obstacle detected!");

            // Check if game over logic has already been triggered to prevent repeat actions
            if (!isGameOver)
            {
                isGameOver = true; // Prevent further triggers

                // Call OnGameOver to save the high score
                if (playerScore != null)
                {
                    playerScore.OnGameOver();
                }

                // Start the game-over sequence
                StartCoroutine(GameOverSequence());
            }
        }
        else
        {
            Debug.Log("No game-ending collision detected.");
        }
    }

    // Coroutine to handle game-over delay and restart
    private IEnumerator GameOverSequence()
    {
        // Play the game-over sound only once and only after a valid collision
        if (gameOverSound != null)
        {
            gameOverSound.Play();
        }
        else
        {
            Debug.LogWarning("Game over sound is not assigned!");
        }

        // Stop time (pause gameplay, not audio)
        Time.timeScale = 0;

        // Wait for the specified delay (scaled by real-time, not game time)
        yield return new WaitForSecondsRealtime(delayBeforeRestart);

        // Resume time
        Time.timeScale = 1;

        // Restart the scene
        Debug.Log("Restarting scene: " + SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: Log when the player enters the trigger zone to verify if it's being entered
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player is within trigger range of the obstacle.");
        }
    }

    // Optional: Log when the player exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player exited trigger range of the obstacle.");
        }
    }
}