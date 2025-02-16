using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrophyCollector : MonoBehaviour
{
    public GameObject player;
    public GameObject trophy;
    public Canvas winCanvas;
    public TextMeshProUGUI WinText;  // Reference to the TextMeshProUGUI component inside the UI Panel
    public TextMeshProUGUI levelText; // Reference to the TextMeshProUGUI component to display level
    public Vector3 playerStartPosition;
    public Vector3 trophyStartPosition;
    public GameObject[] walls; // Array of walls in the scene
    public GameObject[] coins; // Array of coins in the scene
    public float resetDelay = 6f;
    public DoubleSlider doubleSlider; // Reference to the DoubleSlider script for resetting the timer
    public float trophyDelay = 1f; // Delay before reactivating the trophy
    public TextMeshProUGUI gameOverText; // TMP text to display "Game Over"
    private PlayerMovement playerMovement;
    private Rigidbody playerRigidbody;
    private int currentLevel = 1;

    void Start()
    {
        // Ensure the canvas and text setup is correct
        if (winCanvas != null)
        {
            winCanvas.enabled = false; // Hide win canvas initially
        }

        if (levelText != null)
        {
            levelText.enabled = false; // Hide level text initially
        }

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRigidbody = player.GetComponent<Rigidbody>();
            playerStartPosition = player.transform.position;
        }

        if (trophy != null)
        {
            trophyStartPosition = trophy.transform.position;
        }

        // Set up walls
        if (walls != null && walls.Length > 0)
        {
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false); // Deactivate walls initially
            }
        }

        // Set up coins
        if (coins != null && coins.Length > 0)
        {
            foreach (GameObject coin in coins)
            {
                coin.SetActive(false); // Deactivate coins initially
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Show the level text (TextMeshProUGUI) inside the panel
            if (levelText != null)
            {
                levelText.enabled = true;
                levelText.text = "Level " + currentLevel + " Completed!"; // Set the level message
            }

            // Disable player movement and interactions
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
            }

            // Hide the trophy and move to next level
            if (trophy != null)
            {
                trophy.SetActive(false);
            }

            // Reset the timer for the next level
            if (doubleSlider != null)
            {
                doubleSlider.ResetTimer(); // Reset the time when moving to the next level
            }

            // Load the next level after a delay
            Invoke(nameof(LoadNextLevel), resetDelay);
        }
    }

    private void LoadNextLevel()
    {
        // Increment the level counter
        currentLevel++;
        if(currentLevel>10){
              if (gameOverText != null)
        {
            gameOverText.enabled = true; // Show TMP text when game is over
            gameOverText.text = " You win!"; // You can customize the message if you want
            Time.timeScale = 0f;
        }
        }
        // Reset player position and other elements
        if (player != null)
        {
            player.transform.position = playerStartPosition;

            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }

        if (levelText != null)
        {
            levelText.enabled = false; // Hide the level text after resetting
        }

        // Deactivate all walls and coins at the start of the new level
        if (walls != null)
        {
            foreach (var wall in walls)
            {
                wall.SetActive(false); // Deactivate all walls
            }
        }

        if (coins != null)
        {
            foreach (var coin in coins)
            {
                coin.SetActive(false); // Deactivate all coins
            }
        }

        // Calculate how many walls and coins should be active for the current level
        int wallsToActivate = Mathf.Min((2 * currentLevel), walls.Length); // Double the level, but cap it at walls.Length
        int coinsToActivate = Mathf.Min(currentLevel, coins.Length); // Coins to activate will be based on the current level

        // Randomize wall activation
        List<int> activatedWalls = new List<int>(); // To track activated walls
        while (activatedWalls.Count < wallsToActivate)
        {
            int randomIndex = Random.Range(0, walls.Length);
            if (!activatedWalls.Contains(randomIndex))
            {
                activatedWalls.Add(randomIndex);
                walls[randomIndex].SetActive(true); // Activate the wall
            }
        }

        // Randomize coin activation
        List<int> activatedCoins = new List<int>(); // To track activated coins
        while (activatedCoins.Count < coinsToActivate)
        {
            int randomIndex = Random.Range(0, coins.Length);
            if (!activatedCoins.Contains(randomIndex))
            {
                activatedCoins.Add(randomIndex);
                coins[randomIndex].SetActive(true); // Activate the coin
            }
        }

        // Debugging: log activated walls and coins count
        Debug.Log("Level " + currentLevel + " - Activated " + activatedWalls.Count + " walls.");
        Debug.Log("Level " + currentLevel + " - Activated " + activatedCoins.Count + " coins.");

        // Delayed trophy activation after the reset
        Invoke(nameof(ActivateTrophy), trophyDelay);
    }

    private void ActivateTrophy()
    {
        // Reactivate the trophy after the delay
       
        
            trophy.SetActive(true);
            trophy.transform.position = trophyStartPosition; // Reset trophy position for new level
        
    }
}
