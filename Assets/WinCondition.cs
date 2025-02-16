using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrophyCollector : MonoBehaviour
{
    public GameObject player;
    public GameObject trophy;
    public Canvas winCanvas;
    public TextMeshProUGUI WinText;
    public TextMeshProUGUI levelText;
    public Vector3 playerStartPosition;
    public Vector3 trophyStartPosition;
    public GameObject[] walls;
    public GameObject[] coins;
    public float resetDelay = 6f;
    public DoubleSlider doubleSlider;
    public float trophyDelay = 1f;
    public TextMeshProUGUI gameOverText;
    private PlayerMovement playerMovement;
    private Rigidbody playerRigidbody;
    private int currentLevel = 1;

    void Start()
    {
        if (winCanvas != null)
        {
            winCanvas.enabled = false;
        }

        if (levelText != null)
        {
            levelText.enabled = false;
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

        if (walls != null && walls.Length > 0)
        {
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false);
            }
        }

        if (coins != null && coins.Length > 0)
        {
            foreach (GameObject coin in coins)
            {
                coin.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (levelText != null)
            {
                levelText.enabled = true;
                levelText.text = "Level " + currentLevel + " Completed!";
            }

            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
            }

            if (trophy != null)
            {
                trophy.SetActive(false);
            }

            if (doubleSlider != null)
            {
                doubleSlider.ResetTimer();
            }

            Invoke(nameof(LoadNextLevel), resetDelay);
        }
    }

    private void LoadNextLevel()
    {
        currentLevel++;
        if(currentLevel>10){
            if (gameOverText != null)
            {
                gameOverText.enabled = true;
                gameOverText.text = " You win!";
                Time.timeScale = 0f;
            }
        }
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
            levelText.enabled = false;
        }

         if (walls != null)
    {
        foreach (var wall in walls)
        {
            if (wall != null) 
            {
                wall.SetActive(false);
            }
        }
    }

    if (coins != null)
    {
        foreach (var coin in coins)
        {
            if (coin != null) 
            {
                coin.SetActive(false);
            }
        }
    }
        int wallsToActivate = Mathf.Min((2 * currentLevel), walls.Length);
        int coinsToActivate = Mathf.Min(currentLevel, coins.Length);
// Activate walls
        if (walls != null && walls.Length > 0)
        {
            List<int> activatedWalls = new List<int>();
            while (activatedWalls.Count < wallsToActivate)
            {
                int randomIndex = Random.Range(0, walls.Length);
                if (!activatedWalls.Contains(randomIndex) && walls[randomIndex] != null)
                {
                    activatedWalls.Add(randomIndex);
                    walls[randomIndex].SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogWarning("Walls array is not assigned or is empty!");
        }

        // Activate coins
        if (coins != null && coins.Length > 0)
        {
            List<int> activatedCoins = new List<int>();
            while (activatedCoins.Count < coinsToActivate)
            {
                int randomIndex = Random.Range(0, coins.Length);
                if (!activatedCoins.Contains(randomIndex) && coins[randomIndex] != null)
                {
                    activatedCoins.Add(randomIndex);
                    coins[randomIndex].SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogWarning("Coins array is not assigned or is empty!");
        }

        Invoke(nameof(ActivateTrophy), trophyDelay);
    }

    private void ActivateTrophy()
    {
        trophy.SetActive(true);
        trophy.transform.position = trophyStartPosition;
    }
}
