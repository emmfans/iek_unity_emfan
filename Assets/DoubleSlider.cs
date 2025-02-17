 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoubleSlider : MonoBehaviour
{
    public RectTransform GreenBar;
    public RectTransform RedBar;

    public float maxTime = 10f;
    public float cointime = 10f;
    public float timeDecrementOnEnemyTouch = 1f;
    public TextMeshProUGUI gameOverText;

    private float elapsedTime = 0f;
    private float futuretime = 0f;
    private float timeLeft;
    private float barWidth;
     public TextMeshProUGUI startText;
    private RectTransform parentRect;

    public TrophyCollector trophyCollector;
     private bool gameStarted = false;
     public GameObject player;
     private Vector3 playerStartPosition;
     

    void Start()
    {
       
        timeLeft = maxTime;
        elapsedTime = 0f;
        futuretime = 0f;
        Time.timeScale = 0f;
        parentRect = GreenBar.parent.GetComponent<RectTransform>();
        barWidth = parentRect.rect.width;

        GreenBar.pivot = new Vector2(0.0f, 0.5f);
        RedBar.pivot = new Vector2(1.0f, 0.5f);

        GreenBar.sizeDelta = new Vector2(0, GreenBar.sizeDelta.y);
        RedBar.sizeDelta = new Vector2(0, RedBar.sizeDelta.y);

        RedBar.anchoredPosition = new Vector2(barWidth, RedBar.anchoredPosition.y);

        if (gameOverText != null)
        {
            gameOverText.enabled = false;
        }
          if (player != null)
        {
            playerStartPosition = player.transform.position;  // Save player start position
        }
    }

    void Update()
    {
         if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            
             if (startText != null)
            

         {
            startText.enabled = false;
            }
            if (gameOverText != null)
        {
            gameOverText.enabled = false;
           
        }
             if (player != null)
            {
                player.transform.position = playerStartPosition;
            }
             
            ResetTimer();
            trophyCollector.resetGame();
          
        }
        if (elapsedTime < maxTime && timeLeft > 0)
        {
            elapsedTime += Time.deltaTime;
            futuretime += Time.deltaTime;

            timeLeft = maxTime - futuretime;

            float greenBarWidth = barWidth * (elapsedTime / maxTime);
            float redBarWidth = barWidth * (futuretime / maxTime);

            GreenBar.sizeDelta = new Vector2(greenBarWidth, GreenBar.sizeDelta.y);
            RedBar.sizeDelta = new Vector2(redBarWidth, RedBar.sizeDelta.y);

            RedBar.anchoredPosition = new Vector2(barWidth - redBarWidth, RedBar.anchoredPosition.y);

            if (greenBarWidth >= (barWidth - redBarWidth))
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.enabled = true;
            gameOverText.text = "Game Over!";
            player.transform.position = playerStartPosition;
            
        }
         if (startText != null)
        {
            startText.enabled = true;
            startText.text = "Press Space to Start";
        }
        Time.timeScale = 0f;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        futuretime = 0f;
        timeLeft = maxTime;

        GreenBar.sizeDelta = new Vector2(0, GreenBar.sizeDelta.y);
        RedBar.sizeDelta = new Vector2(0, RedBar.sizeDelta.y);

        RedBar.anchoredPosition = new Vector2(barWidth, RedBar.anchoredPosition.y);
    }

    public void AddTime()
    {
        
        futuretime = Mathf.Max(futuretime - cointime, 0);
        float redBarWidth = barWidth * (futuretime / maxTime);
        RedBar.sizeDelta = new Vector2(redBarWidth, RedBar.sizeDelta.y);
        RedBar.anchoredPosition = new Vector2(barWidth - redBarWidth, RedBar.anchoredPosition.y);
    }
}