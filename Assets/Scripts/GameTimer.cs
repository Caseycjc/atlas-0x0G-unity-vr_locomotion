using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public float minuteDuration = 60f; // Duration of a game minute in real-time seconds
    private float timer;
    private int hour;
    public delegate void TimeChanged(string time);
    public event TimeChanged OnTimeChanged;

    void Start()
    {
        timer = 0f;
        hour = 12; // Game starts at 12 AM
        UpdateTimeDisplay();
        Debug.Log("GameTimer started");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= minuteDuration)
        {
            timer = 0f;
            hour++;
            if (hour > 12)
                hour = 1; // Reset hour to 1 after 12

            UpdateTimeDisplay();
            OnNewHour();
        }

        if (hour == 6 && timer == 0f)
        {
            WinGame();
        }

        // Test WinGame method
        if (Input.GetKeyDown(KeyCode.W))
        {
            WinGame();
        }
    }

    void UpdateTimeDisplay()
    {
        string currentTime = hour + " AM";
        timeDisplay.text = currentTime;
        OnTimeChanged?.Invoke(currentTime); // Notify listeners
        Debug.Log("Time updated: " + currentTime);
    }

    void OnNewHour()
    {
        Debug.Log("New hour reached: " + hour);
        // Reset player health, respawn zombies
        var playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
            Debug.Log("Player health reset");
        }
        var zombieManager = FindObjectOfType<ZombieManager>();
        if (zombieManager != null)
        {
            zombieManager.RespawnZombies();
            Debug.Log("Zombies respawned");
        }
    }

    void WinGame()
    {
        Debug.Log("WinGame called");
        SceneManager.LoadScene("WinScreen");
    }

    public string GetCurrentTime()
    {
        return hour + " AM";
    }
}
