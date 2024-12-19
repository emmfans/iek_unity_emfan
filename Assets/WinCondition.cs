using UnityEngine;

public class TrophyCollector : MonoBehaviour
{
    public GameObject player; 
    public GameObject trophy; 
    public Canvas winCanvas; 
    public Vector3 playerStartPosition; 
    public Vector3 trophyStartPosition; 
    public float resetDelay = 6f; 
    private PlayerMovement playerMovement; 
    private Rigidbody playerRigidbody; 

    void Start()
    {
        if (winCanvas != null)
        {
            winCanvas.enabled = false;
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
            if (winCanvas != null)
            {
                winCanvas.enabled = true;
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

            Invoke(nameof(ResetGame), resetDelay);
        }
    }

    private void ResetGame()
    {
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

        if (winCanvas != null)
        {
            winCanvas.enabled = false;
        }
    }
}
