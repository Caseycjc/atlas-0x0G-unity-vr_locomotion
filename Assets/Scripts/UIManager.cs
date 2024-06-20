using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text timeText;
    private PlayerHealth playerHealth;
    private GameTimer gameTimer;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameTimer = FindObjectOfType<GameTimer>();

        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHealthText;
            UpdateHealthText(playerHealth.currentHealth);
        }
        else
        {
            UpdateHealthText(10); // Default value if playerHealth is not found
        }

        if (gameTimer != null)
        {
            gameTimer.OnTimeChanged += UpdateTimeText;
            UpdateTimeText(gameTimer.GetCurrentTime()); // Assuming you have a method to get current time as a string
        }
        else
        {
            UpdateTimeText("12 AM"); // Default value if gameTimer is not found
        }
    }

    void UpdateHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }

    void UpdateTimeText(string time)
    {
        timeText.text = time;
    }
}
