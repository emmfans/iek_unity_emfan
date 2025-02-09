using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To handle UI elements like Text
using TMPro; // To handle TextMeshPro elements

public class DoubleSlider : MonoBehaviour
{
    public RectTransform GreenBar;  // Left bar (expanding right)
    public RectTransform RedBar;    // Right bar (expanding left)
    public float maxTime = 10f; // Total time for bars to meet
    public float timeDecrementOnEnemyTouch = 1f; // How much to reduce the time left when touching an enemy

    public TextMeshProUGUI gameOverText; // TMP text to display "Game Over"

    private float elapsedTime = 0f;  // Time passed
    private float timeLeft;  // Time left
    private float barWidth;
    private RectTransform parentRect;

    void Start()
    {
        // Initialize time and bar settings
        timeLeft = maxTime;
        elapsedTime = 0f;

        parentRect = GreenBar.parent.GetComponent<RectTransform>();
        barWidth = parentRect.rect.width; // Get total bar width

        // Set pivots for proper growth behavior
        GreenBar.pivot = new Vector2(0.0f, 0.5f); // GreenBar grows from the left
        RedBar.pivot = new Vector2(1.0f, 0.5f);   // RedBar grows from the right

        // Set initial sizes
        GreenBar.sizeDelta = new Vector2(0, GreenBar.sizeDelta.y);
        RedBar.sizeDelta = new Vector2(0, RedBar.sizeDelta.y); // RedBar starts at 0 width

        // Place RedBar at the right edge (it will grow leftward)
        RedBar.anchoredPosition = new Vector2(barWidth, RedBar.anchoredPosition.y);

        // Disable TMP text at the start
        if (gameOverText != null)
        {
            gameOverText.enabled = false; // Make sure TMP text is hidden initially
        }
    }

    void Update()
    {
        // Only update if the game is not over
        if (elapsedTime < maxTime && timeLeft > 0)
        {
            // Increase the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the time left based on elapsed time
            timeLeft = maxTime - elapsedTime;

            // Calculate the widths of both bars
            // GreenBar grows from left to right based on elapsedTime
            float greenBarWidth = barWidth * (elapsedTime / maxTime);

            // RedBar grows from the right to the left, based on elapsedTime
            float redBarWidth = barWidth * (elapsedTime / maxTime);

            // Apply the new sizes to both bars
            GreenBar.sizeDelta = new Vector2(greenBarWidth, GreenBar.sizeDelta.y);

            // RedBar grows leftward: Adjust the width and its position relative to the right
            RedBar.sizeDelta = new Vector2(redBarWidth, RedBar.sizeDelta.y);

            // The RedBar will grow leftward from the right side
            RedBar.anchoredPosition = new Vector2(barWidth - redBarWidth, RedBar.anchoredPosition.y);

            // Debugging: Log the widths of the bars and their positions
            Debug.Log($"GreenBar Width: {greenBarWidth}, GreenBar Position: {GreenBar.anchoredPosition}");
            Debug.Log($"RedBar Width: {redBarWidth}, RedBar Position: {RedBar.anchoredPosition}");

            // Check if the bars have met (GreenBar end >= RedBar start)
            if (greenBarWidth >= (barWidth - redBarWidth))
            {
                // Pause the game and display game over
                GameOver();
            }
        }
    }

    void GameOver()
    {
        // Display Game Over message
        if (gameOverText != null)
        {
            gameOverText.enabled = true; // Show TMP text when game is over
            gameOverText.text = "Game Over!"; // You can customize the message if you want
        }

        Debug.Log("Game Over! The bars have met.");

        // Pause the game (freeze the gameplay)
        Time.timeScale = 0f;
    }
}
