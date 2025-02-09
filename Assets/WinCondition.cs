using UnityEngine;
using TMPro;  // Import for TextMeshPro components

public class TrophyCollector : MonoBehaviour
{
    public GameObject player;
    public GameObject trophy;
    public Canvas winCanvas;
    public TextMeshProUGUI WinText;  // Reference to the TextMeshProUGUI component inside the UI Panel
    public Vector3 playerStartPosition;
    public Vector3 trophyStartPosition;
    public float resetDelay = 6f;
    private PlayerMovement playerMovement;
    private Rigidbody playerRigidbody;

    void Start()
    {
        // Ensure the canvas and text setup is correct
        if (winCanvas != null)
        {
            winCanvas.enabled = true; // Make sure the canvas is active
        }

        if (WinText != null)
        {
            WinText.enabled = false; // Hide the text initially
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Show the WinText (TextMeshProUGUI) inside the panel
            if (WinText != null)
            {
                WinText.enabled = true;
                WinText.text = "You Win!"; // Set the win message
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

            Invoke(nameof(ResetGame), resetDelay); // Wait for reset
        }
    }

    private void ResetGame()
    {
        // Reset player position and other elements
        if (player != null)
        {
            player.transform.position = playerStartPosition;

            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }

        if (trophy != null)
        {
            trophy.transform.position = trophyStartPosition;
            trophy.SetActive(true);
        }

        if (WinText != null)
        {
            WinText.enabled = false; // Hide the win text after resetting
        }
    }
}
